﻿using System.Text.Json.Serialization;

namespace simple_ecommerce.api.Response
{
    public class ProductBaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorCode { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Error { get; set; }
    }
}
