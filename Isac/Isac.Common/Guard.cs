using System;

namespace Isac.Common
{
    public static class Guard
    {
        public static void AgainstNullArgument<T>(string argumentName, T argument) where T : class
        {
            if (argument == null)
            {
                // TODO, move message to resources
                throw new ArgumentNullException($"{argumentName} is null.", argumentName);
            }
        }
    }
}
