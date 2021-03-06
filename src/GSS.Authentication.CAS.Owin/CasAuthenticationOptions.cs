using System;
using System.Net.Http;
using GSS.Authentication.CAS;
using GSS.Authentication.CAS.Owin;
using GSS.Authentication.CAS.Validation;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;

namespace Owin
{
    /// <summary>
    /// Configuration options for <see cref="CasAuthenticationMiddleware"/>
    /// </summary>
    public class CasAuthenticationOptions : AuthenticationOptions, ICasOptions
    {
        public const string Scheme = CasDefaults.AuthenticationType;

        public CasAuthenticationOptions() : base(CasDefaults.AuthenticationType)
        {
            Caption = CasDefaults.AuthenticationType;
            CallbackPath = new PathString("/signin-cas");
            AuthenticationMode = AuthenticationMode.Passive;
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            CookieManager = new CookieManager();
        }

        /// <summary>
        /// Gets or sets timeout value in milliseconds for back channel communications with CAS server.
        /// </summary>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        /// The HttpMessageHandler used to communicate with CAS server.
        /// This cannot be set at the same time as BackchannelCertificateValidator unless the value 
        /// can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; } = default!;

        /// <summary>
        /// Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get => Description.Caption;
            set => Description.Caption = value;
        }

        /// <summary>
        /// The request path within the application's base path where the user-agent will be returned.
        /// The middleware will process this request when it arrives.
        /// Default value is "/signin-cas".
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        /// Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user <see cref="System.Security.Claims.ClaimsIdentity"/>.
        /// </summary>
        public string? SignInAsAuthenticationType { get; set; }

        /// <summary>
        /// store serviceTicket in AuthenticationProperties for single sign out ?
        /// </summary>
        public bool UseAuthenticationSessionStore { get; set; }

        /// <summary>
        /// Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; } = default!;

        /// <summary>
        /// An abstraction for reading and setting cookies during the authentication process.
        /// </summary>
        public ICookieManager CookieManager { get; set; }

        /// <inheritdoc />
        public string CasServerUrlBase { get; set; } = default!;

        /// <summary>
        /// Gets or sets the service URL
        /// Default will automatic generated by OWIN request
        /// </summary>
        public Uri? ServiceUrlBase { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IServiceTicketValidator"/> used to validate service ticket.
        /// Default is <see cref="Cas30ServiceTicketValidator"/>.
        /// </summary>
        public IServiceTicketValidator ServiceTicketValidator { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="ICasAuthenticationProvider"/> used to handle authentication events.
        /// </summary>
        public ICasAuthenticationProvider Provider { get; set; } = default!;
    }
}
