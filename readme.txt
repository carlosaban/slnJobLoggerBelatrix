1.- Run the script scriptDB\1_tables.sql to the sqlserver database
2.- Update the connectionstring for the "Database" test project. you need to find the section JobLoggerConfigurableModule and update the attribute "Keysource"
3.- Similar procedure for the "File" test project. You need to find the section JobLoggerConfigurableModule and update the attribute "Keysource" with a valid directory for example c:\temp\ . You need to be sure that end with backslash.