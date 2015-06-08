// ***********************************************************************
// Assembly         : ReflectSoftware.Insight.Extensions.CommonLogging
// Author           : ReflectSoftware Inc.
// Created          : 03-19-2014
// Last Modified On : 03-19-2014
// ***********************************************************************
// <copyright file="RIFactoryAdapter.cs" company="ReflectSoftware, Inc.">
//     Copyright (c) ReflectSoftware, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using Common.Logging;

/// <summary>
/// Namespace - ReflectSoftware.Insight.Extensions.CommonLogging
/// </summary>
namespace ReflectSoftware.Insight.Extensions.CommonLogging
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   RIFactoryAdapter Class. </summary>
    /// <remarks>   ReflectInsight Version 5.3. </remarks>
    ///
    /// <seealso cref="T:Common.Logging.ILoggerFactoryAdapter"/>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class RIFactoryAdapter : ILoggerFactoryAdapter
    {
        ///--------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes a new instance of the <see cref="RIFactoryAdapter"/> class. </summary>
        ///
        /// <remarks>   ReflectInsight Version 5.3. </remarks>
        ///
        /// <param name="args"> Arguments</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RIFactoryAdapter(params object[] args)
        {
        }

        ///--------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes a new instance of the <see cref="RIFactoryAdapter"/> class. </summary>
        ///
        /// <remarks>   ReflectInsight Version 5.3. </remarks>
        ///
        /// <param name="properties">   The
        ///                             <see cref="System.Collections.Specialized.NameValueCollection"/>
        ///                             properties. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RIFactoryAdapter(NameValueCollection properties)
        {
            // parse config properties
            // string configType = ArgUtils.GetValue(properties, "configType", string.Empty).ToUpper();
            // string configFile = ArgUtils.GetValue(properties, "configFile", string.Empty);
        }
        ///--------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Get an ILog instance by name. </summary>
        ///
        /// <remarks>   ReflectInsight Version 5.3. </remarks>
        ///
        /// <seealso cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(string)"/>
        /// ### <param name="name"> The name of the logger. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ILog GetLogger(string name)
        {
            return new RICommonLogger(name);
        }
        ///--------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Get an ILog instance by type. </summary>
        ///
        /// <remarks>   ReflectInsight Version 5.3. </remarks>
        ///
        /// <seealso cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(Type)"/>
        /// ### <param name="type"> The type to use for the logger. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ILog GetLogger(Type type)
        {
            return new RICommonLogger(type);
        }
    }
}
