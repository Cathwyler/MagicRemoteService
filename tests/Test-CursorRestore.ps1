$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class CursorProbe {
 [StructLayout(LayoutKind.Sequential)] public struct Info { public bool icon; public uint x,y; public IntPtr mask,color; }
 [DllImport("user32.dll")] public static extern bool SystemParametersInfo(uint action,uint param,IntPtr value,uint flags);
 [DllImport("user32.dll")] public static extern IntPtr LoadCursor(IntPtr module,IntPtr name);
 [DllImport("user32.dll")] public static extern bool GetIconInfo(IntPtr cursor,out Info info);
 [DllImport("gdi32.dll")] public static extern bool DeleteObject(IntPtr value);
}
"@
function Get-CursorSize {
 $info = New-Object CursorProbe+Info
 if (![CursorProbe]::GetIconInfo([CursorProbe]::LoadCursor([IntPtr]::Zero,[IntPtr]32512),[ref]$info)) { throw 'GetIconInfo failed' }
 $bitmap = $null
 try {
  $handle = if ($info.color -ne [IntPtr]::Zero) { $info.color } else { $info.mask }
  $bitmap = [Drawing.Bitmap]::FromHbitmap($handle)
  $height = if ($info.color -ne [IntPtr]::Zero) { $bitmap.Height } else { $bitmap.Height / 2 }
  return "$($bitmap.Width)x$height"
 } finally {
  if ($bitmap) { $bitmap.Dispose() }
  if ($info.mask -ne [IntPtr]::Zero) { [CursorProbe]::DeleteObject($info.mask) | Out-Null }
  if ($info.color -ne [IntPtr]::Zero) { [CursorProbe]::DeleteObject($info.color) | Out-Null }
 }
}
if (![CursorProbe]::SystemParametersInfo(0x57,0,[IntPtr]::Zero,0)) { throw 'Theme reload failed' }
$before = Get-CursorSize
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $PSScriptRoot '..\MagicRemoteService\bin\Release\MagicRemoteService.exe'))
$type = $assembly.GetType('MagicRemoteService.SystemCursor',$true)
$flags = [Reflection.BindingFlags]'Public,Static'
try {
 $type.GetMethod('SetMagicRemoteServiceSystemCursor',$flags).Invoke($null,@()) | Out-Null
 $type.GetMethod('SetDefaultSystemCursor',$flags).Invoke($null,@()) | Out-Null
 $after = Get-CursorSize
 if ($before -ne $after) { throw "Cursor size changed: $before -> $after" }
 $type.GetMethod('SetDefaultSystemCursor',$flags).Invoke($null,@()) | Out-Null
 if ((Get-CursorSize) -ne $before) { throw 'Repeated cleanup changed cursor size' }
 Write-Output "PASS: compiled cursor replacement/restoration: $before -> $after; repeated cleanup OK"
} finally { [CursorProbe]::SystemParametersInfo(0x57,0,[IntPtr]::Zero,0) | Out-Null }
