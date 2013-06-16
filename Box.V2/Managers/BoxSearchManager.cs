﻿using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxSearchManager : BoxResourceManager
    {
        public BoxSearchManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }


        /// <summary>
        /// Returns a collection of search results that match the keyword, if there are are no matching search results
        /// an empty collection will be returned
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<BoxCollection<BoxItem>> SearchAsync(string keyword, int limit, int offset = 0, List<string> fields = null)
        {
            keyword.ThrowIfNullOrWhiteSpace("keyword");

            BoxRequest request = new BoxRequest(_config.SearchEndpointUri)
                .Param("query", keyword)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request);
                    
            return response.ResponseObject;
        }

    }
}