using System;

namespace DddWorkshops.Common.Guard
{
    public static class Guard
    {
        /// <summary>
        ///     Specifies <typeparamref name="TException" /> which will be thrown when assertion fails.
        /// </summary>
        /// <typeparam name="TException">Type of exception to be thrown upon assertion failure.</typeparam>
        /// <returns>Internal implementation of <see cref="IGuardContext{TException}" />, used to verify assertion.</returns>
        public static IGuardContext<TException> With<TException>() where TException : Exception => new GuardContext<TException>();

        /// <summary>
        ///     Verifies assertion.
        /// </summary>
        /// <typeparam name="TException">Type of exception to be thrown upon assertion failure.</typeparam>
        private class GuardContext<TException> : IGuardContext<TException> where TException : Exception
        {
            /// <inheritdoc />
            public void Against(bool expression, string? message = null)
            {
                if (expression)
                {
                    throw (TException)Activator.CreateInstance(typeof(TException), message ?? "Assertion failed!")!;
                }
            }

            /// <inheritdoc />
            public void Against(bool expression, params object[] arguments)
            {
                if (expression)
                {
                    throw (TException)Activator.CreateInstance(typeof(TException), arguments)!;
                }
            }
        }
    }
}