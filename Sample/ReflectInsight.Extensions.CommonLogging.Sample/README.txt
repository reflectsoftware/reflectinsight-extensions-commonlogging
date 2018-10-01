Common.Logging Extension Sample

This sample demonstrates how to use the ReflectInsight Common.Logging Extension. The purpose of this sample is to show you how to leverage ReflectInsight with an pre-existing logging framework.

This sample also uses satellite configuration files, which are a practice that we recommend doing. For more information about satellite configuration files, please
see the satellite configuration sample and/or our online resource.

To get started with the Common.Logging Extension, please do the following:

1. Start by adding an application configuration file (app.config) to your project. If you already have one, then proceed to step #2.

2. Next add/update the configSections with a section for for "insightSettings" in addition to what is required for the Common.Logging framework:

  <configuration>
	  <configSections>
		<section name="insightSettings" type="ReflectSoftware.Insight.ConfigurationHandler,ReflectSoftware.Insight"/>
		<sectionGroup name="common">
		  <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
		</sectionGroup>
	  </configSections>
  </configuration>

3. Then add a new section called <insightSettings></insightSettings> and add the externalConfigSource to point to your external ReflectInsight.config file:

  <configuration>
	<insightSettings externalConfigSource="ReflectInsight.config" />
  </configuration>

4. Please make sure you update the ReflectInsight.config file property 'Copy to Output Directory' to 'Copy always'.
  
5. Next ensure you have configured you application with the applicable Common.Logging configuration.

6. In your Common.Logging configuration, add the following section

	<common>
	  <logging>
		<factoryAdapter type="ReflectSoftware.Insight.Extensions.CommonLogging.RIFactoryAdapter, ReflectSoftware.Insight.Extensions.CommonLogging">
		  <arg key="configType" value="FILE" />
		</factoryAdapter>
	  </logging>
	</common>


7. Finally, you will need to configure the ReflectInsight.config file. 

	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
		<insightSettings>
		<baseSettings>
			<configChange enabled="true" />
			<propagateException enabled="false" />
			<exceptionEventTracker time="20" />
			<debugMessageProcess enabled="true" />
		</baseSettings>

		<files default="">
		  <autoSave name="DefaultSave" onNewDay="true" onMsgLimit="1000000" onSize="0" recycleFilesEvery="30" />
		</files>

		<listenerGroups active="Debug">
			<group name="Debug" enabled="true" maskIdentities="false">
			<destinations>
				<destination name="Viewer" enabled="true" filter="" details="Viewer" />
				<destination name="BinaryFile" enabled="true" filter="" details="BinaryFile[path=$(workingdir)\Logs\Log.rlg; autoSave=DefaultSave]" />
			</destinations>
			</group>
		</listenerGroups>
	</configuration>

8. That's it your done!


Additional Resources

Getting started with the CommonLogging configuration
https://reflectsoftware.atlassian.net/wiki/display/RI5/ReflectInsight+5+documentation

Knowledge Base
http://reflectsoftware.uservoice.com/knowledgebase

User Feedback
http://reflectsoftware.uservoice.com/forums/158277-reflectinsight-feedback

Support
support@reflectsoftware.com