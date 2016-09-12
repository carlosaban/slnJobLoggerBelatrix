using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLogger
{
    /// <summary>
    /// JobLogger is a good candidate for use the singleton pattern. We only need an instance of this class.

    //I added some additional classes in order to centralize the configurations settings. I added a section in the config file in order to improve.

    //I have removed some appsetting and I moved them to the new section.
    /// </summary>
    public class JobLogger
    {

        

        #region Attributes
            private bool _logToFile;
            private bool _logToConsole;
            private bool _logMessage;
            private bool _logWarning;
            private bool _logError;
            private bool _logToDatabase; //I have update the name in order to have the same pattern to the others
            // private bool _initialized; // this attribute never is used
            private static JobLogger _instance;
            private ConfigurationLoggger _ConfigurationLogger;
            //aditional attributes
            private string _ConnectionString;
            private string _LogFileDirectory;
        
        #endregion
        #region Properties
            /// <summary>
            /// Instance is the property that return the instance of the JobLogger
            /// </summary>
            public static  JobLogger Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new JobLogger();
                    }

                    return _instance;

                }
            }
            

            public ConfigurationLoggger ConfigurationLogger
            {
                get { return _ConfigurationLogger; }
            }
            

            #endregion
        #region Constructors and Initialization methods
                private void InitLogger()
                {

                    this._ConfigurationLogger = ConfigurationLoggger.GetConfiguration();

                    switch (ConfigurationLogger.Loggertype.Type.ToUpper() )
                    {
                        case "FILE":
                            if (string.IsNullOrWhiteSpace(ConfigurationLogger.Loggertype.KeySource)) throw new Exception("ConfigurationLogger: Directory is not valid");
                            _logToFile = true;
                            this._LogFileDirectory = ConfigurationLogger.Loggertype.KeySource;
                            break;
                        case "DATABASE":
                            if (string.IsNullOrWhiteSpace(ConfigurationLogger.Loggertype.KeySource)) throw new Exception("ConfigurationLogger: DB connection is not valid");
                            _logToDatabase = true;
                            this._ConnectionString = ConfigurationLogger.Loggertype.KeySource;
                            break;
                        case "CONSOLE": _logToConsole = true;
                            break;
                        default:
                            //In some part of the code I'm a little redundant in order to do more easy for review. 
                            _logToFile = false;
                            _logToDatabase = false;
                            _logToConsole = false;
                            break;
                    }

                    switch (ConfigurationLogger.Level.ToUpper())
                    {
                        case "ERROR":
                            this._logError = true;
                            break;
                        case "WARN":
                            this._logError = true;
                            this._logWarning = true;
                            break;
                        case "MESSAGE":
                            this._logError = true;
                            this._logWarning = true;
                            this._logMessage = true;
                            break;

                        default: 
                            this._logError = false;
                            this._logWarning = false;
                            this._logMessage = false;
                            break;
                    }

                }


                private JobLogger()
                {
                    InitLogger();

                }
            #endregion
        #region Methods
                    /// <summary>
                    /// print the log in different sources
                    /// </summary>
                    /// <param name="message"></param>
                    /// <param name="bmessage">I found an error in the name of this parameter I have updated the name to bmessage</param>
                    /// <param name="warning"></param>
                    /// <param name="error"></param>
                    public bool LogMessage(string message, bool bmessage, bool warning, bool error)
                    {

                        message.Trim();
                        if (message == null || message.Length == 0)
                        {
                            return false; //Only for test I wwill return false if the message is empty. The definition depend of our funcionality definitions
                        }
                        if (!_logToConsole && !_logToFile && !_logToDatabase)
                        {
                            throw new LoggerExceptionType();
                        }
                        if ((!_logError && !_logMessage && !_logWarning) || (!bmessage && !warning && !error))
                        {
                            throw new LoggerExceptionLevel();
                        }
                        
                        try
                        {
                           
                            //There is an error here, We need to use the conditions in order to execute the correct part of code . If the Type is DB you need to execute the Database connection

                            if (_logToDatabase)
                            {
                                //Here we don't use a correct use of the connection we need to be sure that we will release the resource after that we use the database.

                                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(this._ConnectionString))
                                {
                                    connection.Open();
                                    //connection.BeginTransaction();
                                    int t = 0;
                                    if (bmessage && _logMessage)
                                    {
                                        t = 1;
                                    }
                                    else if (warning && _logWarning)
                                    {
                                        t = 3;
                                    }
                                    else if (error && _logError)
                                    {
                                        t = 2;
                                    }
                                    if ((error && _logError) || (warning && _logWarning) || (bmessage && _logMessage))
                                    {
                                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Insert into Log Values('" + DateTime.Now.ToString()+ " " + message + "', " + t.ToString() + ")", connection);
                                        command.ExecuteNonQuery();

                                    }

                                }




                            }
                            if (this._logToFile)
                            {
                                string l = string.Empty;
                                //there is an error here, the file need to be opened if exist. On the other hand I will add a local variable for the compleate filename.

                                string compleateLogFile = _LogFileDirectory + "LogFile" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";

                                if (System.IO.File.Exists(compleateLogFile))
                                {
                                    //l = System.IO.File.ReadAllText(_LogFileDirectory + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"); original line
                                    l = System.IO.File.ReadAllText(compleateLogFile);
                                }
                               
                                if ((error && _logError) || (warning && _logWarning) || (bmessage && _logMessage))
                                {
                                    l = l + DateTime.Now.ToString() + message + Environment.NewLine;
                                    System.IO.File.WriteAllText(compleateLogFile, l);
                                }
                            }
                            // In this case if we not exclude the condition the message will be repeated. I have added an "else" for each condition
                            if (this._logToConsole)
                            {

                                if (bmessage && _logMessage)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else if (warning && _logWarning)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                else if (error && _logError)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                //Here there is an error too. we need to add a condition in order to only print with the respective level restriction
                                if ((error && _logError) || (warning && _logWarning) || (bmessage && _logMessage))
                                    Console.WriteLine(DateTime.Now.ToShortDateString() + message);


                            }

                            return true;
                        }
                        catch (Exception )
                        {
                            //in this case in order to an inner error not affect the process we could return a boolean result in order to know for some error.
                            return false;
                        }
                       
                        
                    }

                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="message"></param>
                    /// <param name="level"></param>
                    public bool LogMessage(string message, LoggerLevelEnum level)
                    {

                        bool lmessage = false;
                        bool lwarning = false;
                        bool lerror = false;
                        switch (level)
                        {
                            case LoggerLevelEnum.ERROR:
                                lerror = true;
                                break;
                            case LoggerLevelEnum.WARN:
                                lwarning = true;
                                break;
                            case LoggerLevelEnum.MESSAGE:
                                lmessage = true;
                                break;
                            default: break;
                        }
                        return LogMessage(message, lmessage, lwarning, lerror);

                    }
                #endregion


     }
    }