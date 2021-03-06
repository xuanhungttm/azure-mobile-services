﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.WindowsAzure.MobileServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #if __UNIFIED__
    using Foundation;
    #else
    using MonoTouch.Foundation;
    #endif

    /// <summary>
    /// Define a class help to create/update/delete notification registrations
    /// </summary>
    public sealed class Push
    {
        internal readonly IRegistrationManager RegistrationManager;

        internal Push(MobileServiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            
            var storageManager = new LocalStorageManager(client.ApplicationUri.Host);
            var pushHttpClient = new PushHttpClient(client);
            this.RegistrationManager = new RegistrationManager(pushHttpClient, storageManager);
        }

        internal Push(IRegistrationManager registrationManager)
        {
            if (registrationManager == null)
            {
                throw new ArgumentNullException("registrationManager");
            }

            this.RegistrationManager = registrationManager;
        }

        /// <summary>
        /// PLEASE USE NSData overload of this method!! Register a particular deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterNativeAsync(string deviceToken)
        {
            return this.RegisterNativeAsync(deviceToken, null);
        }

        /// <summary>
        /// Register a particular deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterNativeAsync(NSData deviceToken)
        {
            return this.RegisterNativeAsync(deviceToken, null);
        }

        /// <summary>
        /// PLEASE USE NSData overload of this method!! Register a particular deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="tags">The tags to register to receive notifications from</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterNativeAsync(string deviceToken, IEnumerable<string> tags)
        {
            if (string.IsNullOrWhiteSpace(deviceToken))
            {
                throw new ArgumentNullException("deviceToken");
            }

            var registration = new ApnsRegistration(deviceToken, tags);
            return this.RegistrationManager.RegisterAsync(registration);
        }

        /// <summary>
        /// Register a particular deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="tags">The tags to register to receive notifications from</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterNativeAsync(NSData deviceToken, IEnumerable<string> tags)
        {
            if (deviceToken == null)
            {
                throw new ArgumentNullException("deviceToken");
            }

            var registration = new ApnsRegistration(deviceToken, tags);
            return this.RegistrationManager.RegisterAsync(registration);
        } 

        /// <summary>
        /// PLEASE USE NSData overload of this method!! Register a particular deviceToken with a template
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="jsonTemplate">The string defining the template</param>
        /// <param name="expiry">The string defining the expiry template</param>
        /// <param name="templateName">The template name</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterTemplateAsync(string deviceToken, string jsonTemplate, string expiry, string templateName)
        {
            return this.RegisterTemplateAsync(deviceToken, jsonTemplate, expiry, templateName, null);
        }

        /// <summary>
        /// Register a particular deviceToken with a template
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="jsonTemplate">The string defining the template</param>
        /// <param name="expiry">The string defining the expiry template</param>
        /// <param name="templateName">The template name</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterTemplateAsync(NSData deviceToken, string jsonTemplate, string expiry, string templateName)
        {
            return this.RegisterTemplateAsync(deviceToken, jsonTemplate, expiry, templateName, null);
        }

        /// <summary>
        /// PLEASE USE NSData overload of this method!! Register a particular deviceToken with a template
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="jsonTemplate">The string defining the json template</param>
        /// <param name="expiry">The string defining the expiry template</param>
        /// <param name="templateName">The template name</param>
        /// <param name="tags">The tags to register to receive notifications from</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterTemplateAsync(string deviceToken, string jsonTemplate, string expiry, string templateName, IEnumerable<string> tags)
        {
            if (string.IsNullOrWhiteSpace(deviceToken))
            {
                throw new ArgumentNullException("deviceToken");
            }

            if (string.IsNullOrWhiteSpace(jsonTemplate))
            {
                throw new ArgumentNullException("jsonTemplate");
            }

            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            var registration = new ApnsTemplateRegistration(deviceToken, jsonTemplate, expiry, templateName, tags);
            return this.RegistrationManager.RegisterAsync(registration);
        }

        /// <summary>
        /// Register a particular deviceToken with a template
        /// </summary>
        /// <param name="deviceToken">The deviceToken to register</param>
        /// <param name="jsonTemplate">The string defining the json template</param>
        /// <param name="expiry">The string defining the expiry template</param>
        /// <param name="templateName">The template name</param>
        /// <param name="tags">The tags to register to receive notifications from</param>
        /// <returns>Task that completes when registration is complete</returns>
        public Task RegisterTemplateAsync(NSData deviceToken, string jsonTemplate, string expiry, string templateName, IEnumerable<string> tags)
        {
            if (deviceToken == null)
            {
                throw new ArgumentNullException("deviceToken");
            }

            if (string.IsNullOrWhiteSpace(jsonTemplate))
            {
                throw new ArgumentNullException("jsonTemplate");
            }

            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            var registration = new ApnsTemplateRegistration(deviceToken, jsonTemplate, expiry, templateName, tags);
            return this.RegistrationManager.RegisterAsync(registration);
        }

        /// <summary>
        /// Unregister any registrations previously registered from this device
        /// </summary>
        /// <returns>Task that completes when unregister is complete</returns>
        public Task UnregisterNativeAsync()
        {
            return this.UnregisterTemplateAsync(Registration.NativeRegistrationName);
        }

        /// <summary>
        /// Unregister any registrations with given templateName registered from this device
        /// </summary>
        /// <param name="templateName">The template name</param>
        /// <returns>Task that completes when unregister is complete</returns>
        public Task UnregisterTemplateAsync(string templateName)
        {
            return this.RegistrationManager.UnregisterAsync(templateName);
        }

        /// <summary>
        /// DEBUG-ONLY: PLEASE USE NSData overload of this method!! Unregister any registrations with given deviceToken
        /// </summary>
        /// <param name="deviceToken">The device token</param>
        /// <returns>Task that completes when unregister is complete</returns>
        public Task UnregisterAllAsync(string deviceToken)
        {
            if (string.IsNullOrWhiteSpace(deviceToken))
            {
                throw new ArgumentNullException("deviceToken");
            }

            return this.RegistrationManager.DeleteRegistrationsForChannelAsync(ApnsRegistration.TrimDeviceToken(deviceToken));
        }

        /// <summary>
        /// DEBUG-ONLY: Unregister any registrations with given deviceToken
        /// </summary>
        /// <param name="deviceToken">The device token</param>
        /// <returns>Task that completes when unregister is complete</returns>
        public Task UnregisterAllAsync(NSData deviceToken)
        {
            if (deviceToken == null)
            {
                throw new ArgumentNullException("deviceToken");
            }

            return this.RegistrationManager.DeleteRegistrationsForChannelAsync(ApnsRegistration.ParseDeviceToken(deviceToken));
        }

        /// <summary>
        /// Register for notifications
        /// </summary>
        /// <param name="registration">The object defining the registration</param>
        /// <returns>Task that will complete when the registration is completed</returns>
        public Task RegisterAsync(Registration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            if (string.IsNullOrWhiteSpace(registration.PushHandle))
            {
                throw new ArgumentNullException("registration.PushHandle");
            }

            registration.PushHandle = ApnsRegistration.TrimDeviceToken(registration.PushHandle);
            return this.RegistrationManager.RegisterAsync(registration);
        }

        /// <summary>
        /// DEBUG-ONLY: PLEASE USE NSData overload of this method!! List the registrations made with the service for a deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to check for</param>
        /// <returns>List of registrations</returns>
        public Task<List<Registration>> ListRegistrationsAsync(string deviceToken)
        {
            return this.RegistrationManager.ListRegistrationsAsync(ApnsRegistration.TrimDeviceToken(deviceToken));
        }

        /// <summary>
        /// DEBUG-ONLY: List the registrations made with the service for a deviceToken
        /// </summary>
        /// <param name="deviceToken">The deviceToken to check for</param>
        /// <returns>List of registrations</returns>
        public Task<List<Registration>> ListRegistrationsAsync(NSData deviceToken)
        {
            return this.RegistrationManager.ListRegistrationsAsync(ApnsRegistration.ParseDeviceToken(deviceToken));
        }
    }
}