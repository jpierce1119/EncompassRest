﻿using EncompassREST.PipelineModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncompassREST
{
    public class Pipeline
    {
        private Session _session;


        public Session Session
        {
            get { return _session; }
        }

        public Pipeline(Session Session)
        {
            _session = Session;
        }


        public async Task<string> postPipelineQueryAsync(IFilter filter,List<SortOrderItem> sortItem,List<string> fields)
        {
            var obj = new
            {
                filter = filter,
                sortOrder = sortItem,
                fields = fields
            };

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, string.Format("loans/Pipeline"));
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            message.Content = new StringContent(JsonConvert.SerializeObject(obj,settings), Encoding.UTF32, "applicaiotn/json");

            var response = await _session.RESTClient.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new EncompassREST.Exceptions.RESTException("postPipelineQueryAsync", response);
            }
            
        }



    }
}
