<b>Words from the author</b>

Author : Justin Butterworth - jub@milestone.dk

This plugin is provided as is and with minimal documentation and testing.
The plugin is designed to be installed in the management client and to allow the searching of devices/hardware on the system by name (Recording server/hardware/device), IP of the device and MAC of the device.
You can also filter by enabled/disabled devices as needed.
This plugin is not optimized and may take a few minutes to populate the list on larger systems.

<b>Installation</b>
(installed on the management Client)

Create a new directory under 
C:\Program Files\Milestone\MIPPlugins
for example 
camerasearch

Paste the plugin.def and the Device Search.dll into that folder.
Start the management Client

<b>Uninstall</b>

Incase of issues please stop the mangement client enter the folder you created in
C:\Program Files\Milestone\MIPPlugins\
and rename the plugin.def to plugin.def.disabled to stop the plugin from being loaded.

<b>Instructions</b>

You'll find a new mip plugin -> camerasearch -> camera Search option in your MC, selecting this will start a background load of the camera config.
Once started you can select other tabs and do other work while it loads all devices, on all hardware on all Recording servers.

Once loaded you can do a freeform search in the top search bar, refresh will reload the config for the search plugin. You can include disabled devices with the check box. 
The table can have its columns ordered or changed to suit and the total grid can be exported with the export to CSV button on the right (this will only export what is displayed)

<b>Logging</b>

There is performance logging in the MIPTrace.log, usually located in C:\ProgramData\Milestone\XProtect Management Client\Logs

<b>Troubleshooting</b>

You may find the the dlls are blocked by windows, in this case, right click on the dll, select properties and uncheck the "blocked" check box.
