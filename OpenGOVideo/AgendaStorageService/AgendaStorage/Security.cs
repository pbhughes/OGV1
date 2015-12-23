using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace AgendaServices
{

    [AttributeUsage(AttributeTargets.Method)]
    public class AuthentExtendedAttribute : SoapExtensionAttribute
    {
        int _priority = 1;
        public override Type ExtensionType
        {
            get { return typeof(AuthentExtension); }
        }

        public override int Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }

        public DateTime x;

    }

    public class AuthentExtension : SoapExtension
    {
       


        public override object GetInitializer(Type serviceType)
        {
            return GetType();
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
   
            return null;
        }

        public override void Initialize(object initializer)
        {

        }

        public override void ProcessMessage(SoapMessage message)
        {
            //Implement Authentication mechanism here


            try
            {
                switch (message.Stage)
                {
                    case SoapMessageStage.AfterDeserialize:


                        AuthenTokenHeader tokenHdr = null;
                        string token = string.Empty;

                        foreach (SoapHeader hdr in message.Headers)
                        {
                            if (hdr is AuthenTokenHeader)
                                tokenHdr = (AuthenTokenHeader)hdr;
                        }

                        if (tokenHdr == null)
                            throw new SoapException("No Authentication Header Present", SoapException.ClientFaultCode);



                        if (AuthenticateToken(ref tokenHdr))
                            return;  //granted
                        else
                        {
                            //authentication was attempted but access denied.
                            throw new SoapException("Authentication failed", SoapException.ClientFaultCode);
                        }

                        break;



                    default:
                        break;

                }




            }

            catch (Exception ex)
            {

                throw new SoapException(string.Format("Authentication processed but errored: {0}", ex.Message), SoapException.ClientFaultCode, ex);
            }

        }

        private bool AuthenticateToken(ref AuthenTokenHeader TokenHeader)
        {
            if (TokenHeader.Token == null)
                return false;

            string pathToSources = HttpContext.Current.Server.MapPath("~/sources.config");
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(pathToSources);
            foreach(XmlNode node in xdoc.SelectNodes("//Source"))
            {

                if (node.Attributes["Key"].Value.ToString() == TokenHeader.Token)
                {
                    TokenHeader.Path = node.Attributes["Path"].Value.ToString();
                    return true;
                }
            }


            return false;

        }

    }


    public class AuthenTokenHeader : SoapHeader
    {
        public string Token { get; set; }
        public string Path { get; set; }
    }

}
