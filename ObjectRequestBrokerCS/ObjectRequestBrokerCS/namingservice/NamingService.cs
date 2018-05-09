using System;
using System.Collections.Generic;
using System.Net;

namespace namingservice
{
    using Marshaller = orbapi.Marshaller;
    using Entry = requestreplyapi.entries.Entry;
    using ExtendedEntry = requestreplyapi.entries.ExtendedEntry;
    using Replyer = requestreplyapi.requestreply.Replyer;
    using InvalidRequestReply = namingservice.replies.InvalidRequestReply;
    using LocalizationReply = namingservice.replies.LocalizationReply;
    using RegistrationReply = namingservice.replies.RegistrationReply;
    using Reply = namingservice.replies.Reply;
    using LocalizationRequest = namingservice.requests.LocalizationRequest;
    using RegistrationRequest = namingservice.requests.RegistrationRequest;


    public class NamingService
    {
        private static NamingService instance = new NamingService();
        public static Entry NAMING_SERVICE_ENTRY;

        static NamingService()
        {
            NAMING_SERVICE_ENTRY = new Entry(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), 1111);
        }

        private const string NAMING_SERVICE_NAME = "NamingService";
        private Dictionary<string, ExtendedEntry> entryMap;
        private bool isServiceRunning;

        public static NamingService Instance
        {
            get { return instance; }
        }

        private NamingService()
        {
            entryMap = new Dictionary<string, ExtendedEntry>();
            isServiceRunning = false;
        }

        public virtual void startService()
        {
            if (isServiceRunning)
            {
                Console.WriteLine("NamingService already running");
                return;
            }

            Replyer mReplyer = new Replyer(NAMING_SERVICE_NAME, NAMING_SERVICE_ENTRY);
            isServiceRunning = true;

            while (isServiceRunning)
            {
                mReplyer.receive_transform_and_send_feedback(@in =>
                {
                    Reply reply;
                    string inStr = Marshaller.stringFromByteArray(@in);
                    if (inStr.Contains("registration_request"))
                    {
                        reply = processRegistrationRequest(@in);
                    }
                    else if (inStr.Contains("localization_request"))
                    {
                        reply = processLocalizationRequest(@in);
                    }
                    else
                    {
                        reply = new InvalidRequestReply();
                    }

                    return Marshaller.marshallObject(reply);
                });
            }
        }

        private Reply processLocalizationRequest(byte[] @in)
        {
            Reply reply;
            LocalizationRequest locRequest = (LocalizationRequest) Marshaller.unMarshallObject(@in);

            reply = new LocalizationReply("localization_reply", entryMap[locRequest.Entry_name],
                entryMap[locRequest.Entry_name] != null);
            return reply;
        }

        private Reply processRegistrationRequest(byte[] @in)
        {
            Reply reply;
            RegistrationRequest regRequest = (RegistrationRequest) Marshaller.unMarshallObject(@in);
            bool req_res;
            if (entryMap.ContainsKey(regRequest.Entry_name))
            {
                req_res = false;
            }
            else
            {
                entryMap[regRequest.Entry_name] = regRequest.Entry_data;
                req_res = true;
            }

            reply = new RegistrationReply("registration_reply", req_res);
            return reply;
        }

        public void stopService()
        {
            if (!isServiceRunning)
            {
                Console.WriteLine("NamingService is not running!");
                return;
            }

            isServiceRunning = false;
        }

        public static void main(string[] args)
        {
            Instance.startService();
        }
    }
}