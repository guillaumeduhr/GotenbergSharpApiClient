﻿//  Copyright 2019-2022 Chris Mohan, Jaben Cargman
//  and GotenbergSharpApiClient Contributors
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using JetBrains.Annotations;

namespace Gotenberg.Sharp.API.Client.Domain.Requests.Facets.UrlExtras;

public class ExtraUrlResources : IConvertToHttpContent
{
    public List<ExtraUrlResourceItem> Items { get; [UsedImplicitly] set; } = new();

    public IEnumerable<HttpContent> ToHttpContent()
    {
        foreach (var g in Items.GroupBy(i => i.FormDataFieldName))
        {
            var groupValue = string.Join(", ", g.Select(gi => gi!.ToJson()));
            yield return BuildRequestBase.CreateFormDataItem($"[{groupValue}]", g.Key);
        }
    }
}