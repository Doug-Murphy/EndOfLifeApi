# Introduction

The End of Life API is an Azure Functions project written to be a single source of truth that can be used
programmatically to determine whether or not a particular Target Framework Moniker (TFM) is end of life (EOL). It
provides a very simple, yet fully feature-encompassing set of endpoints. Both the legacy csproj TFM style (eg. v4.5) and
the newer SDK-style csproj TFM style (eg. net45) are supported.

# Endpoints

## /api/check-eol/:tfm

This endpoint checks if the specific TFM is EOL or not. The TFM argument can be a single TFM or a semicolon-delimited
list. If none of the specified TFM's are EOL, this will return 204-No Content. If any of the specified TFM's are EOL,
then any EOL TFM of those specified will be returned in a JSON object with an array named `EndOfLifeTargetFrameworks`.

Example: When passing `net45;net6.0` for the TFM.

```json
{
    "EndOfLifeTargetFrameworks": {
        "net45": "2016-01-12T00:00:00"
    }
}
```

## /api/get-all-eol?timeframeUnit=\<enum>&timeframeAmount=\<byte>

This endpoint will return a JSON object with an array named `EndOfLifeTargetFrameworks` containing all TFM's that are
currently EOL, or will be EOL within the (optional) query-string parameter timeframe. The results are sorted
alphabetically to provide easier manual parsing.

```json
{
    "EndOfLifeTargetFrameworks": {
        "net11": "2011-07-12T00:00:00",
        "net20": "2011-07-12T00:00:00",
        "net30": "2011-07-12T00:00:00",
        "net40": "2016-01-12T00:00:00",
        "net403": "2016-01-12T00:00:00",
        "net45": "2016-01-12T00:00:00",
        "net451": "2016-01-12T00:00:00",
        "netcoreapp1.0": "2019-06-27T00:00:00",
        "netcoreapp1.1": "2019-06-27T00:00:00",
        "netcoreapp2.0": "2018-10-01T00:00:00",
        "netcoreapp2.1": "2021-08-21T00:00:00",
        "netcoreapp2.2": "2019-12-23T00:00:00",
        "netcoreapp3.0": "2020-03-03T00:00:00",
        "v1.1": "2011-07-12T00:00:00",
        "v2.0": "2011-07-12T00:00:00",
        "v3.0": "2011-07-12T00:00:00",
        "v4.0": "2016-01-12T00:00:00",
        "v4.0.3": "2016-01-12T00:00:00",
        "v4.5": "2016-01-12T00:00:00",
        "v4.5.1": "2016-01-12T00:00:00"
    }
}
```

# Usage
View the [public Postman workspace](https://www.postman.com/HDougMurphy/workspace/end-of-life-api) for the collection of
endpoints and environments for use.
