﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.Store.Cmdlet
{
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Security.Permissions;
    using Microsoft.Samples.WindowsAzure.ServiceManagement;
    using Microsoft.WindowsAzure.Management.Cmdlets.Common;
    using Microsoft.WindowsAzure.Management.Store.Model;
    using System.Linq;

    /// <summary>
    /// Create scaffolding for a new node web role, change cscfg file and csdef to include the added web role
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureStoreAvailableAddOn"), OutputType(typeof(List<WindowsAzureOffer>))]
    public class GetAzureStoreAvailableAddOnCommand : CloudBaseCmdlet<IServiceManagement>
    {
        public MarketplaceClient MarketplaceClient { get; set; }

        [Parameter(Position = 0, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Country code")]
        [ValidateCountryLengthAttribute()]
        public string Country { get; set; }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override void ExecuteCmdlet()
        {
            LocationList locations = Channel.ListLocations(CurrentSubscription.SubscriptionId);
            MarketplaceClient = MarketplaceClient ?? 
                new MarketplaceClient(locations.Select<Location, string>(l => l.Name));
            List<WindowsAzureOffer> result = MarketplaceClient.GetAvailableWindowsAzureOffers(Country);
            WriteObject(result ?? new List<WindowsAzureOffer>(), true);
        }
    }
}