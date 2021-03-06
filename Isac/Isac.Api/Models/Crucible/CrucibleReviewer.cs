﻿using Isac.WebHooks.Receivers.BitbucketServer.Models.Converters;
using Newtonsoft.Json;
using System;

namespace Isac.Api.Models.Crucible
{
    public class CrucibleReviewer
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("avatarUrl")]
        public Uri AvatarUri { get; set; }

        [JsonProperty("completed")]
        public bool HasCompleted { get; set; }

        [JsonProperty("completionStatusChangeDate")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset CompletedDate { get; set; }

        [JsonProperty("timeSpent")]
        // TODO, create a timespan converter to handle this
        public string TimeSpent { get; set; }
    }
}
