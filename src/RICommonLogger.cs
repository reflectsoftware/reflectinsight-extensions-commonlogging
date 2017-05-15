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

        private readonly IReflectInsight FReflectInsight;

        /// <summary>
        /// Initializes the <see cref="RICommonLogger"/> class.
        /// </summary>
        /// <seealso cref="M:Common.Logging.Factory.AbstractLogger.GetWriteHandler" />
        static RICommonLogger()
        {
            FSendInternalErrorMethodInfo = typeof(ReflectInsight).GetMethod("SendInternalError", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RICommonLogger"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal RICommonLogger(String name)
        {
            FReflectInsight = RILogManager.Get(name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RICommonLogger"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        internal RICommonLogger(Type type): this(type.FullName)
        {
        }

        /// <summary>
        /// Sends the internal error.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="mType">Type of the m.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        static private Boolean SendInternalError(IReflectInsight ri, MessageType mType, Exception ex)
        {
            return (Boolean)FSendInternalErrorMethodInfo.Invoke(ri, new object[] { mType, ex });
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="mType">Type of the m.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
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

        /// <summary>
        /// Internal function to write a message.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">the message to log.</param>
        /// <param name="exception">the exception to log (may be null)</param>
        /// <seealso cref="M:Common.Logging.Factory.AbstractLogger.WriteInternal(LogLevel,Object,Exception)" />
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

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Debug" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsDebugEnabled" />
        public override bool IsDebugEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Error" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsErrorEnabled" />
        public override bool IsErrorEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Fatal" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsFatalEnabled" />
        public override bool IsFatalEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Info" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsInfoEnabled" />
        public override bool IsInfoEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Trace" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsTraceEnabled" />
        public override bool IsTraceEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Checks if this logger is enabled for the
        /// <see cref="F:Common.Logging.LogLevel.Warn" /> level.
        /// </summary>
        /// <remarks>
        /// Override this in your derived class to comply with the underlying logging system.
        /// </remarks>
        /// <seealso cref="P:Common.Logging.Factory.AbstractLogger.IsWarnEnabled" />
        public override bool IsWarnEnabled
        {
            get { return true; }
        }
    }
}
