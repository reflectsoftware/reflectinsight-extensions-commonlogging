/******************************************************************************
*
* Copyright (c) ReflectSoftware, Inc. All rights reserved. 
*
* See License.rtf in the solution root for license information.
*
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Common.Logging;
using Common.Logging.Factory;

using ReflectSoftware.Insight.Common;
using RI.Utils.ExceptionManagement;


namespace ReflectSoftware.Insight.Extensions.CommonLogging
{
    ///------------------------------------------------------------------------
    /// <summary>   RICommonLogger Class. </summary>
    /// <seealso cref="T:Common.Logging.Factory.AbstractLogger"/>
    ///------------------------------------------------------------------------
    public class RICommonLogger : AbstractLogger
    {
        static private readonly MethodInfo FSendInternalErrorMethodInfo;

        private readonly ReflectInsight FReflectInsight;

        ///--------------------------------------------------------------------
        static RICommonLogger()
        {
            FSendInternalErrorMethodInfo = typeof(ReflectInsight).GetMethod("SendInternalError", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
        }        
        ///--------------------------------------------------------------------
        internal RICommonLogger(String name)
        {
            FReflectInsight = RILogManager.Get(name);
        }

        ///--------------------------------------------------------------------
        internal RICommonLogger(Type type): this(type.FullName)
        {
        }

        ///--------------------------------------------------------------------
        static private Boolean SendInternalError(ReflectInsight ri, MessageType mType, Exception ex)
        {
            return (Boolean)FSendInternalErrorMethodInfo.Invoke(ri, new object[] { mType, ex });
        }

        ///--------------------------------------------------------------------
        private void SendMessage(MessageType mType, String message, Exception exception)
        {
            try
            {
                // build details
                StringBuilder sb = null;

                if (exception != null)
                {
                    sb = new StringBuilder();
                    sb.Append(ExceptionBasePublisher.ConstructIndentedMessage(exception));
                    sb.AppendLine();
                    sb.AppendLine();
                }

                String details = sb != null ? sb.ToString() : null;
                FReflectInsight.Send(mType, message, details);
            }
            catch(Exception ex)
            {
                if (!SendInternalError(FReflectInsight, mType, ex)) throw;
            }
        }
        ///--------------------------------------------------------------------
        /// <summary>   Internal function to write a message. </summary>
        ///
        /// <seealso cref="M:Common.Logging.Factory.AbstractLogger.WriteInternal(LogLevel,Object,Exception)"/>
        /// ### <param name="level">        the level of this log event. </param>
        /// ### <param name="message">      the message to log. </param>
        /// ### <param name="exception">    the exception to log (may be null) </param>
        ///--------------------------------------------------------------------
        protected override void WriteInternal(LogLevel logLevel, Object message, Exception exception)
        {
            if (logLevel == LogLevel.Off)
                return;

            String strMessage = message.ToString();

            if (logLevel == LogLevel.Info)
            {
                if (strMessage.StartsWith("[Enter]"))
                {
                    FReflectInsight.EnterMethod(strMessage.Replace("[Enter]", String.Empty));
                    return;
                }
                if (strMessage.StartsWith("[Exit]"))
                {
                    FReflectInsight.ExitMethod(strMessage.Replace("[Exit]", String.Empty));
                    return;
                }

                SendMessage(MessageType.SendInformation, strMessage, exception);
            }
            else if (logLevel == LogLevel.Trace)
            {
                SendMessage(MessageType.SendTrace, strMessage, exception);
            }
            else if (logLevel == LogLevel.Debug) 
            {
                SendMessage(MessageType.SendDebug, strMessage, exception);
            }
            else if (logLevel == LogLevel.Warn) 
            {
                SendMessage(MessageType.SendWarning, strMessage, exception);
            }
            else if (logLevel == LogLevel.Error) 
            {
                SendMessage(MessageType.SendError, strMessage, exception);
            }
            else if (logLevel == LogLevel.Fatal) 
            {
                SendMessage(MessageType.SendFatal, strMessage, exception);
            }
            else // safetynet
            {
                SendMessage(MessageType.SendMessage, strMessage, exception);
            }
        }
        ///--------------------------------------------------------------------
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Debug" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsDebugEnabled"/>
        ///--------------------------------------------------------------------
        public override bool IsDebugEnabled
        {
            get { return true; }
        }
        ///--------------------------------------------------------------------
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Error" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsErrorEnabled"/>        
        ///--------------------------------------------------------------------
        public override bool IsErrorEnabled
        {
            get { return true; }
        }
        ///--------------------------------------------------------------------        
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Fatal" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsFatalEnabled"/>
        ///--------------------------------------------------------------------
        public override bool IsFatalEnabled
        {
            get { return true; }
        }
        ///--------------------------------------------------------------------        
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Info" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsInfoEnabled"/>
        ///--------------------------------------------------------------------
        public override bool IsInfoEnabled
        {
            get { return true; }
        }
        ///--------------------------------------------------------------------        
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Trace" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsTraceEnabled"/>
        ///--------------------------------------------------------------------
        public override bool IsTraceEnabled
        {
            get { return true; }
        }
        ///--------------------------------------------------------------------       
        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Warn" /> level.
        /// </summary>
        ///
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        ///
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsWarnEnabled"/>
       ///--------------------------------------------------------------------
        public override bool IsWarnEnabled
        {
            get { return true; }
        }
    }
}
