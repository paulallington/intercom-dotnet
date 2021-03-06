﻿using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;

namespace Intercom.Data
{
	public class Event : Model
	{
		public string event_name { get; set; }
		public long? created_at { get; set; }
		public string user_id { get; set; }
		public string email { get; set; }

		[JsonConverter(typeof(MetadataJsonConverter))]
		public Metadata metadata { get; set; }

		public Event()
		{
		}
	}
}