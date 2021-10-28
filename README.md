# Introduction

The End of Life API is an ASP.NET Core Web API project written to be a single source of truth that can be used
programmatically to determine whether or not a particular Target Framework Moniker (TFM) is end of life (EOL). It
provides a very simple, yet fully feature-encompassing set of endpoints. Both the legacy csproj TFM style (eg. v4.5) and
the newer SDK-style csproj TFM style (eg. net45) are supported.

# Endpoints

## /EndOfLife/check-eol/:tfm

This endpoint checks if the specific TFM is EOL or not. The TFM argument can be a single TFM or a semicolon-delimited
list. If none of the specified TFM's are EOL, this will return 204-No Content. If any of the specified TFM's are EOL,
then any EOL TFM of those specified will be returned in a JSON object with an array named `endOfLifeTargetFrameworks`.

Example: When passing `net45;net5.0` for the TFM.

```json
{
    "endOfLifeTargetFrameworks": [
        "net45"
    ]
}
```

## /EndOfLife/get-all-eol?timeframeUnit=\<enum>&timeframeAmount=\<byte>

This endpoint will return a JSON object with an array named `endOfLifeTargetFrameworks` containing all TFM's that are
currently EOL, or will be EOL within the (optional)
query-string parameter timeframe. The results are sorted alphabetically to provide easier manual parsing.

```json
{
    "endOfLifeTargetFrameworks": [
        "net11",
        "net20",
        "net30",
        "net40",
        "net403",
        "net45",
        "net451",
        "net452",
        "net46",
        "net461",
        "netcoreapp1.0",
        "netcoreapp1.1",
        "netcoreapp2.0",
        "netcoreapp2.1",
        "netcoreapp2.2",
        "netcoreapp3.0",
        "v1.1",
        "v2.0",
        "v3.0",
        "v4.0",
        "v4.0.3",
        "v4.5",
        "v4.5.1",
        "v4.5.2",
        "v4.6",
        "v4.6.1"
    ]
}
```

# Usage

TODO: Insert Postman collection link.
