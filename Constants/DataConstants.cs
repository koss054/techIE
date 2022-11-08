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
        /// Constants for the Official and Marketplace Product entities.
        /// </summary>
        public class Product
        {
            public const int MinNameLength = 10;
            public const int MaxNameLength = 100;

            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 10000;
        }

        /// <summary>
        /// Constants for the extension to the Identity User.
        /// </summary>
        public class User
        {

        }
    }
}
