$ErrorActionPreference = 'Stop'
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class MouseProbe {
 [DllImport("user32.dll", EntryPoint="SystemParametersInfoW")] public static extern bool GetSpeed(uint a,uint b,ref int value,uint flags);
 [DllImport("user32.dll", EntryPoint="SystemParametersInfoW")] public static extern bool SetSpeed(uint a,uint b,int value,uint flags);
 [DllImport("user32.dll", EntryPoint="SystemParametersInfoW")] public static extern bool Mouse(uint a,uint b,[In,Out] int[] value,uint flags);
}
"@
function Get-Speed {
 $speed = 0
 if (![MouseProbe]::GetSpeed(0x70,0,[ref]$speed,0)) { throw 'Cannot read mouse speed' }
 return $speed
}
$original = Get-Speed
$accel = New-Object int[] 3
if (![MouseProbe]::Mouse(3,0,$accel,0)) { throw 'Cannot read acceleration' }
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $PSScriptRoot '..\MagicRemoteService\bin\Release\MagicRemoteService.exe'))
$type = $assembly.GetType('MagicRemoteService.SystemCursor',$true)
$set = $type.GetMethod('SetMagicRemoteServiceMouseSpeedAccel')
$restore = $type.GetMethod('SetDefaultMouseSpeedAccel')
try {
 foreach ($baseline in @(7,13)) {
  if (![MouseProbe]::SetSpeed(0x71,0,$baseline,0)) { throw 'Cannot set test speed' }
  $set.Invoke($null,@()) | Out-Null
  if ((Get-Speed) -ne 10) { throw 'Remote speed was not applied' }
  $set.Invoke($null,@()) | Out-Null
  $restore.Invoke($null,@()) | Out-Null
  if ((Get-Speed) -ne $baseline) { throw 'Original speed was not restored' }
  $afterAccel = New-Object int[] 3
  [MouseProbe]::Mouse(3,0,$afterAccel,0) | Out-Null
  if (($afterAccel -join ',') -ne ($accel -join ',')) { throw 'Acceleration was not restored' }
  $restore.Invoke($null,@()) | Out-Null
  if ((Get-Speed) -ne $baseline) { throw 'Repeated cleanup changed speed' }
  Write-Output "PASS: speed $baseline -> 10 -> $baseline; repeated activation/cleanup and acceleration OK"
 }
} finally {
 [MouseProbe]::SetSpeed(0x71,0,$original,0) | Out-Null
 [MouseProbe]::Mouse(4,0,$accel,0) | Out-Null
}
