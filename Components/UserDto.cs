using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using Newtonsoft.Json;

    public class UserDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user and.
        /// </summary>
        /// <value>
        /// The display name of the user and.
        /// </value>
        [JsonProperty("userAndDisplayName")]
        public string UserAndDisplayName { get; set; }
    }
}