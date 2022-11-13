namespace techIE.Constants
{
    /// <summary>
    /// Class, keeping the constants used for entity validation/requirement.
    /// </summary>
    public class DataConstants
    {
        /// <summary>
        /// Constants for the Category entity.
        /// </summary>
        public class Category
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;
        }

        /// <summary>
        /// Constants for the Product entity.
        /// </summary>
        public class Product
        {
            public const int MinNameLength = 10;
            public const int MaxNameLength = 100;

            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 10000;

            public const string MinPriceValue = "0.00";
            public const string MaxPriceValue = "10000.00";
        }

        /// <summary>
        /// Constants for the extension to the Identity User.
        /// </summary>
        public class User
        {
            public const int MinUserNameLength = 4;
            public const int MaxUserNameLength = 32;

            public const int MinEmailLength = 8;
            public const int MaxEmailLength = 80;

            public const int MinPasswordLength = 12;
            public const int MaxPasswordLength = 32;

            public const string DisplayUserName = "Username";
        }
    }
}
