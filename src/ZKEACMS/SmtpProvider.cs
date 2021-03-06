﻿using Easy.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using ZKEACMS.Setting;
using System.Net;

namespace ZKEACMS
{
    public class SmtpProvider : ISmtpProvider
    {
        private readonly IApplicationSettingService _applicationSettingService;
        public SmtpProvider(IApplicationSettingService applicationSettingService)
        {
            _applicationSettingService = applicationSettingService;
        }
        public SmtpClient Get()
        {
            string host = _applicationSettingService.Get(SettingKeys.SMTP_Host, "smtp.zkea.net");

            int port;
            int.TryParse(_applicationSettingService.Get(SettingKeys.SMTP_Port, "25"), out port);

            string email = _applicationSettingService.Get(SettingKeys.SMTP_Email, "");
            string password = _applicationSettingService.Get(SettingKeys.SMTP_PassWord, "");

            bool ssl;
            bool.TryParse(_applicationSettingService.Get(SettingKeys.SMTP_UseSSL, "false"), out ssl);

            SmtpClient client = new SmtpClient(host, port);
            client.UseDefaultCredentials = true;
            client.EnableSsl = ssl;
            client.Credentials = new NetworkCredential(email, password);
            return client;
        }
    }
}
