using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Factories;
using Newtonsoft.Json;

namespace Intercom.Clients.Intercom.Clients
{
    public class UserLegacysClient : Client
    {
        // TODO: Implement paging
        private static class UserLegacySortBy
        {
            public const String created_at = "created_at";
            public const String updated_at = "updated_at";
            public const String signed_up_at = "signed_up_at";
        }

        private const String USERS_RESOURCE = "users";
        private const String PERMANENT_DELETE_RESOURCE = "user_delete_requests";

        public UserLegacysClient(RestClientFactory restClientFactory)
            : base(USERS_RESOURCE, restClientFactory)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use UserLegacysClient(RestClientFactory restClientFactory)")]
        public UserLegacysClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, USERS_RESOURCE, authentication)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use UserLegacysClient(RestClientFactory restClientFactory)")]
        public UserLegacysClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, USERS_RESOURCE, authentication)
        {
        }

        public UserLegacy Create(UserLegacy user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.user_id', 'user.email' to create a user.");
            }

            ClientResponse<UserLegacy> result = null;
            result = Post<UserLegacy>(Transform(user));
            return result.Result;
        }

        public UserLegacy Update(UserLegacy user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to update a user.");
            }

            ClientResponse<UserLegacy> result = null;
            result = Post<UserLegacy>(Transform(user));
            return result.Result;
        }

        public UserLegacy CreateOrUpdate(UserLegacy user)
        {
            if (user.custom_attributes != null && user.custom_attributes.Any())
            {
                if (user.custom_attributes.Count > 100)
                    throw new ArgumentException("Maximum of 100 fields.");

                foreach (var attr in user.custom_attributes)
                {
                    if (attr.Key.Contains(".") || attr.Key.Contains("$"))
                        throw new ArgumentException(String.Format("Field names must not contain Periods (.) or Dollar ($) characters. key: {0}", attr.Key));

                    if (attr.Key.Length > 190)
                        throw new ArgumentException(String.Format("Field names must be no longer than 190 characters. key: {0}", attr.Key));

                    if (attr.Value == null)
                        throw new ArgumentException(String.Format("'value' is null. key: {0}", attr.Key));
                }
            }

            ClientResponse<UserLegacy> result = null;
            result = Post<UserLegacy>(Transform(user));
            return result.Result;
        }

        public UserLegacy View(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument should include user_id parameter.");
            }

            ClientResponse<UserLegacy> result = null;

            result = Get<UserLegacy>(parameters: parameters);
            return result.Result;
        }

        public UserLegacy View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<UserLegacy> result = null;
            result = Get<UserLegacy>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public UserLegacy View(UserLegacy user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<UserLegacy> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Get<UserLegacy>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.user_id);
                result = Get<UserLegacy>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Get<UserLegacy>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;
        }

        public UserLegacy Archive(UserLegacy user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<UserLegacy> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Delete<UserLegacy>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.user_id);
                result = Delete<UserLegacy>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Delete<UserLegacy>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;
        }

        [Obsolete("Replaced by Archive(UserLegacy user). Renamed for consistency with API language.")]
        public UserLegacy Delete(UserLegacy user)
        {
            return Archive(user);
        }

        public UserLegacy Archive(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<UserLegacy> result = null;
            result = Delete<UserLegacy>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        [Obsolete("Replaced by Archive(String id). Renamed for consistency with API language.")]
        public UserLegacy Delete(String id)
        {
            return Archive(id);
        }

        public UserLegacy UpdateLastSeenAt(String id, long timestamp)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (timestamp <= 0)
            {
                throw new ArgumentException("'timestamp' argument should be bigger than zero.");
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, last_request_at = timestamp });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy UpdateLastSeenAt(UserLegacy user, long timestamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (timestamp <= 0)
            {
                throw new ArgumentException("'timestamp' argument should be bigger than zero.");
            }

            String body = String.Empty;

            if (!String.IsNullOrEmpty(user.id))
                body = JsonConvert.SerializeObject(new { id = user.id, last_request_at = timestamp });
            else if (!String.IsNullOrEmpty(user.user_id))
                body = JsonConvert.SerializeObject(new { user_id = user.user_id, last_request_at = timestamp });
            else if (!String.IsNullOrEmpty(user.email))
                body = JsonConvert.SerializeObject(new { email = user.email, last_request_at = timestamp });
            else
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to update a user's last seen at.");

            ClientResponse<UserLegacy> result = null;
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy UpdateLastSeenAt(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, update_last_request_at = true });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy UpdateLastSeenAt(UserLegacy user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            String body = String.Empty;

            if (!String.IsNullOrEmpty(user.id))
                body = JsonConvert.SerializeObject(new { id = user.id, update_last_request_at = true });
            else if (!String.IsNullOrEmpty(user.user_id))
                body = JsonConvert.SerializeObject(new { user_id = user.user_id, update_last_request_at = true });
            else if (!String.IsNullOrEmpty(user.email))
                body = JsonConvert.SerializeObject(new { email = user.email, update_last_request_at = true });
            else
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to update a user's last seen at.");

            ClientResponse<UserLegacy> result = null;
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy IncrementUserLegacySession(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, new_session = true });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy IncrementUserLegacySession(String id, List<String> companyIds)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            if (!companyIds.Any())
            {
                throw new ArgumentException("'companyIds' shouldnt be empty.");
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, new_session = true, companies = companyIds.Select(c => new { id = c }) });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy RemoveCompanyFromUserLegacy(String id, List<String> companyIds)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            if (!companyIds.Any())
            {
                throw new ArgumentException("'companyIds' shouldnt be empty.");
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = id, companies = companyIds.Select(c => new { id = c, remove = true }) });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy RemoveCompanyFromUserLegacy(UserLegacy user, List<String> companyIds)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (String.IsNullOrEmpty(user.id))
            {
                throw new ArgumentException("'user.id' is null.");
            }

            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            if (!companyIds.Any())
            {
                throw new ArgumentException("'companyIds' shouldnt be empty.");
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { id = user.id, companies = companyIds.Select(c => new { id = c, remove = true }) });
            result = Post<UserLegacy>(body);
            return result.Result;
        }

        public UserLegacy PermanentlyDeleteUserLegacy(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<UserLegacy> result = null;
            String body = JsonConvert.SerializeObject(new { intercom_user_id = id });
            result = Post<UserLegacy>(resource: PERMANENT_DELETE_RESOURCE, body: body);
            return result.Result;
        }

        private String Transform(UserLegacy user)
        {
            var companies = new object();

            if (user.companies != null && user.companies.Any())
            {
                companies = user.companies.Select(c => new
                {
                    remote_created_at = c.remote_created_at,
                    company_id = c.company_id,
                    name = c.name,
                    monthly_spend = c.monthly_spend,
                    custom_attributes = c.custom_attributes,
                    plan = c.plan != null ? c.plan.name : null,
                    website = c.website,
                    size = c.size,
                    industry = c.industry,
                    remove = c.remove
                }).ToList();
            }
            else
                companies = null;

            var body = new
            {
                id = user.id,
                user_id = user.user_id,
                email = user.email,
                phone = user.phone,
                name = user.name,
                companies = companies,
                avatar = user.avatar,
                signed_up_at = user.signed_up_at,
                last_seen_ip = user.last_seen_ip,
                custom_attributes = user.custom_attributes,
                new_session = user.new_session,
                last_seen_user_agent = user.user_agent_data,
                last_request_at = user.last_request_at,
                unsubscribed_from_emails = user.unsubscribed_from_emails,
                referrer = user.referrer,
                utm_campaign = user.utm_campaign,
                utm_content = user.utm_content,
                utm_medium = user.utm_medium,
                utm_source = user.utm_source,
                utm_term = user.utm_term
            };

            return JsonConvert.SerializeObject(body,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}