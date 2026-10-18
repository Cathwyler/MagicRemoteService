# Regression tests

Build the Release configuration with Visual Studio MSBuild and the .NET Framework 4.7.2 Developer Pack after restoring `packages.config` dependencies. Run commands from the repository root.

## Sensor recovery

Requires Node.js. No packages or TV connection are needed:

```powershell
node --test tests/sensor-recovery.cjs
```

The tests execute the sensor subscription function and one-time event listener from the application source with a simulated webOS service. They cover sleeping remotes, pointer wake events, repeated failures, legacy key events, and unrelated subscription failures. Hardware timing still requires validation on a TV.

## Windows regression checks

Run each script in a separate **32-bit Windows PowerShell 5.1** process, matching the executable's default architecture. Close MagicRemoteService and stop its Windows service first so another instance cannot change the same settings during testing.

```powershell
& "$env:WINDIR\SysWOW64\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -File tests/Test-MouseRestore.ps1
& "$env:WINDIR\SysWOW64\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -File tests/Test-CursorRestore.ps1
```

The mouse test temporarily changes pointer speed, exercises repeated activation and cleanup, verifies acceleration restoration, and restores the initial settings in `finally`. The cursor test temporarily replaces the cursors and checks their restored dimensions against the current Windows cursor theme. It reloads that theme on completion.

Run the following from an **administrator PowerShell**:

```powershell
& "$env:WINDIR\SysWOW64\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -File tests/Test-FirstRun.ps1
```

The first-run test invokes the compiled version initialization method. Windows registry redirection applies only inside the test process, targeting a temporary key removed on completion. It checks first-run detection, subsequent startup, and missing/invalid/existing versions. It does not test the settings window visually or modify the installed application's configuration.

## Manual validation

- On a fresh Windows configuration, launch the app and verify that settings open automatically; exit and reopen to verify normal tray startup.
- With the remote asleep, install or launch the TV app, then activate the pointer. Verify motion starts without reinstalling or relaunching. Also check startup with the remote already active.
- Record Windows pointer speed and size, use the TV remote, then exit via the tray. Verify the original speed, acceleration and cursor size return. Repeat the cycle, including on a high-DPI display.
