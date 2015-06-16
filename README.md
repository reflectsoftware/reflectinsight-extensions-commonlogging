# ReflectInsight-Extensions-CommonLogging

[![Build Status](https://ci.appveyor.com/api/projects/status/github/reflectsoftware/reflectinsight-extensions-commonlogging?svg=true)](https://ci.appveyor.com/project/reflectsoftware/reflectinsight-extensions-commonloggin)
[![Release](https://img.shields.io/github/release/reflectsoftware/reflectinsight-extensions-commonlogging.svg)](https://github.com/reflectsoftware/reflectinsight-extensions-commonlogging/releases/latest)
[![NuGet Version](http://img.shields.io/nuget/v/reflectsoftware.insight.extensions.commonlogging.svg?style=flat)](http://www.nuget.org/packages/ReflectSoftware.Insight.Extensions.CommonLogging/)
[![NuGet](https://img.shields.io/nuget/dt/reflectsoftware.insight.extensions.commonlogging.svg)](http://www.nuget.org/packages/ReflectSoftware.Insight.Extensions.CommonLogging/)
[![Stars](https://img.shields.io/github/stars/reflectsoftware/reflectinsight-extensions-commonlogging.svg)](https://github.com/reflectsoftware/reflectinsight-extensions-commonlogging/stargazers)


We've added support for the Common.Logging framework. This allows you to leverage your current investment in the Common.Logging framework, but leverage the power and flexibility that comes with the ReflectInsight viewer. You can view your Common.Logging messages in real-time, in a rich viewer that allows you to filter out and search for what really matters to you.

The Common.Logging extension supports Common.Logging v2.1.2. However if you need to support an older version, then you will need to download the ReflectInsight Logging Extensions Library from GitHub. You can then reference and rebuild the extension against a specific release of the Common.Logging.dll that you plan to use. 

## Getting Started

To install ReflectSoftware.Insight.Extensions.CommonLogging extension, run the following command in the Package Manager Console:

	```powershell
    Install-Package ReflectSoftware.Insight.Extensions.CommonLogging
	```
Then add the following configuration to your app/web configuration file:

    ```xml	
	<?xml version="1.0"?>
	<configuration>
	  <configSections>        
    	<section name="insightSettings" type="ReflectSoftware.Insight.ConfigurationHandler,ReflectSoftware.Insight"/>
    	<sectionGroup name="common">
      		<section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
    	</sectionGroup>
	  </configSections>

	  <common>
    	<logging>
		  <factoryAdapter type="ReflectSoftware.Insight.Extensions.CommonLogging.RIFactoryAdapter, ReflectSoftware.Insight.Extensions.CommonLogging">
        	<arg key="configType" value="FILE" />
      	  </factoryAdapter>
    	</logging>
	  </common>

	  <insightSettings>
    	<baseSettings>
          <configChange enabled="true"/>      
          <propagateException enabled="false"/>      
          <exceptionEventTracker time="20"/>
          <debugMessageProcess enabled="true"/>
        </baseSettings>
        
		<listenerGroups active="Active">
          <group name="Active" enabled="true" maskIdentities="false">
            <destinations>
          	  <destination name="Viewer" enabled="true" filter="" details="Viewer"/>
            </destinations>
          </group>
        </listenerGroups>    
      </insightSettings>
	</configuration>
	```

Addition configuration details for the ReflectSoftware.Insight.Extensions.CommonLogging logging extension can be found [here](https://reflectsoftware.atlassian.net/wiki/display/RI5/ReflectInsight+5+documentation).

## Additional Resources

[Documentation](https://reflectsoftware.atlassian.net/wiki/display/RI5/ReflectInsight+5+documentation)

[Knowledge Base](http://reflectsoftware.uservoice.com/knowledgebase)

[Submit User Feedback](http://reflectsoftware.uservoice.com/forums/158277-reflectinsight-feedback)

[Contact Support](support@reflectsoftware.com)