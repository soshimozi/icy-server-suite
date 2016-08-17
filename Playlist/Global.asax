<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Security.Principal" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        // extract the forms auth cookie
        string cookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authCookie = Context.Request.Cookies[cookieName];
        if (authCookie != null)
        {
            FormsAuthenticationTicket ticket = getAuthTicket(authCookie);
            if (ticket != null)
            {
                FormsIdentity id = new FormsIdentity(ticket);
                GenericPrincipal principal = new GenericPrincipal(id, new string[]{});
                
                // finally attach the new user
                Context.User = principal;
            }
        }    
    }

    FormsAuthenticationTicket getAuthTicket(HttpCookie authCookie)
    {
        FormsAuthenticationTicket ticket = null;
        try
        {
            ticket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch
        { }

        return ticket;
    }
       
</script>
