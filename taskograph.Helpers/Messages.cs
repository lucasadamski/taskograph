﻿namespace taskograph.Helpers
{
    public static class Messages
    {
        //Database 
        public const string DATABASE_OK = "Database OK";
        public const string DATABASE_ERROR_CONNECTION = "Cannot connect to database";
        public const string DATABASE_WRITE_ERROR = "Write operation to Data Base error";

        //Entity Framework
        public const string EF_ERROR_QUERY = "Entity Framework query returned exception";
        public const string EF_ERROR_VIEW = "Entity Framework cannot read view query";

        //Other
        public const string METHOD_EMPTY_PARAMETER = "Method received empty parameter";
        public const string EMPTY_VARIABLE = "Variable is empty or null";
        public const string UNASSIGNED = "-UNASSIGNED-";
        public const int UNASSIGNED_INT = 0;

        public static string GetErrorMessage(string exceptionMessage)
        {
            return $"Exception message: {exceptionMessage}";
        }


        // ************ VIEWS **********************

        //Task 
        public const string NULL_VALUE = "N/A";

        //Error messages
        public const string ERROR_NO_TASKS = "There are no tasks present. Please add at least one task before you continue.";
        public const string ERROR_NO_USER = "No user is signed in. Please sign in.";

    }
}
