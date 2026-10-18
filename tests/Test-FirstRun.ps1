#Requires -RunAsAdministrator
$ErrorActionPreference = 'Stop'
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class RegistryIsolation {
 [DllImport("advapi32.dll")] public static extern int RegOverridePredefKey(IntPtr key, IntPtr replacement);
}
"@
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $PSScriptRoot '..\MagicRemoteService\bin\Release\MagicRemoteService.exe'))
$type = $assembly.GetType('MagicRemoteService.Application', $true)
[Runtime.CompilerServices.RuntimeHelpers]::RunClassConstructor($type.TypeHandle)
[Runtime.CompilerServices.RuntimeHelpers]::RunClassConstructor($assembly.GetType('MagicRemoteService.Program').TypeHandle)
$instance = [Runtime.Serialization.FormatterServices]::GetUninitializedObject($type)
$method = $type.GetMethod('VersionScript', [Reflection.BindingFlags]'Instance,NonPublic')
$testPath = 'Software\MagicRemoteServiceTests-' + [Guid]::NewGuid().ToString('N')
$root = [Microsoft.Win32.Registry]::CurrentUser.CreateSubKey($testPath)
$predefined = if ($assembly.GetType('MagicRemoteService.Program').GetField('bElevated').GetValue($null)) { [IntPtr](-2147483646) } else { [IntPtr](-2147483647) }
$expected = $assembly.GetName().Version.ToString()
try {
 if ([RegistryIsolation]::RegOverridePredefKey($predefined, $root.Handle.DangerousGetHandle()) -ne 0) { throw 'Registry isolation failed' }
 $firstRun = $method.Invoke($instance, @())
 if ($firstRun -ne $true) { throw 'Fresh registry was not detected as first run' }
 if ($method.Invoke($instance, @()) -ne $false) { throw 'Existing registry was incorrectly detected as first run' }
 Write-Output 'PASS: first-run detection before migration; subsequent startup is not first run'
 foreach ($value in @($null, 'invalid-version', $expected)) {
  $key = $root.CreateSubKey('Software\MagicRemoteService')
  if ($null -eq $value) { $key.DeleteValue('Version', $false) } else { $key.SetValue('Version', $value) }
  $key.Dispose()
  $method.Invoke($instance, @()) | Out-Null
  $key = $root.OpenSubKey('Software\MagicRemoteService')
  $actual = $key.GetValue('Version')
  $key.Dispose()
  if ($actual -ne $expected) { throw "Unexpected version: $actual" }
  Write-Output ('PASS: ' + $(if ($null -eq $value) { 'missing version' } else { $value }) + ' -> ' + $actual)
 }
} finally {
 [RegistryIsolation]::RegOverridePredefKey($predefined, [IntPtr]::Zero) | Out-Null
 $root.Dispose()
 [Microsoft.Win32.Registry]::CurrentUser.DeleteSubKeyTree($testPath, $false)
}
