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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}