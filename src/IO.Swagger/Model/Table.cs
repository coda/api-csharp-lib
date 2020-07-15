/* 
 * Coda API
 *
 * # Introduction  The Coda API is a RESTful API that lets you programmatically interact with Coda docs:   * List and search Coda docs  * Create new docs and copy existing ones  * Share and publish docs  * Discover pages, tables, formulas, and controls  * Read, insert, upsert, update, and delete rows  Version 1 of the API will be supported until at least January 15, 2021. As we update and release newer versions of the API, we reserve the right to remove older APIs and functionality with a 3-month deprecation notice. We will post about such changes as well as announce new features in the [Developers Central](https://community.coda.io/c/developers-central) section of our Community, and update the [API updates](https://coda.io/api-updates) doc.  # Getting Started  Our [Getting Started Guide](https://coda.io/t/Getting-Started-Guide-Coda-API_toujpmwflfy) helps you learn the basic of working with the API and shows a few ways you can use it. Check it out, and learn how to:  - Read data from Coda tables and write back to them - Build a one-way sync from one Coda doc to another - Automate reminders - Sync your Google Calendar to Coda  # Using the API  Coda's REST API is designed to be straightforward to use. You can use the language and platform of your choice to make requests. To get a feel for the API, you can also use a tool like [Postman](https://www.getpostman.com/) or [Insomnia](https://insomnia.rest/).  ## API Endpoint  This API uses a base path of `https://coda.io/apis/v1`.  ## Resource IDs and Links  Each resource instance retrieved via the API has the following fields:    - `id`: The resource's immutable ID, which can be used to refer to it within its context   - `type`: The type of resource, useful for identifying it in a heterogenous collection of results   - `href`: A fully qualified URI that can be used to refer to and get the latest details on the resource  Most resources can be queried by their name or ID. We recommend sticking with IDs where possible, as names are fragile and prone to being changed by your doc's users.  ### List Endpoints  Endpoints supporting listing of resources have the following fields:    - `items`: An array containing the listed resources, limited by the `limit` and `pageToken` query parameters   - `nextPageLink`: If more results are available, an API link to the next page of results   - `nextPageToken`: If more results are available, a page token that can be passed into the `pageToken` query parameter  **The maximum page size may change at any time, and may be different for different endpoints.** Please do not rely on it for any behavior of your application. If you pass a `limit` parameter that is larger than our maximum allowed limit, we will only return as many results as our maximum limit. You should look for the presence of the `nextPageToken` on the response to see if there are more results available, rather than relying on a result set that matches your provided limit.  ### Doc IDs  While most object IDs will have to be discovered via the API, you may find yourself frequently wanting to get the ID of a specific Coda doc.  Here's a handy tool that will extract it for you. (See if you can find the pattern!)  <form>   <fieldset style=\"margin: 0px 25px 25px 25px; display: inline;\">     <legend>Doc ID Extractor</legend>     <input type=\"text\" id=\"de_docUrl\" placeholder=\"Paste in a Coda doc URL\"            style=\"width: 250px; padding: 8px; margin-right: 20px;\" />     <span>       Your doc ID is:&nbsp;&nbsp;&nbsp;       <input id=\"de_docId\" readonly=\"true\"              style=\"width: 150px; padding: 8px; font-family: monospace; border: 1px dashed gray;\" />   </fieldset> </form>  <script>   (() => {     const docUrl = document.getElementById('de_docUrl');     const docId = document.getElementById('de_docId');     docUrl.addEventListener('input', () => {       docId.value = (docUrl.value.match(/_d([\\w-]+)/) || [])[1] || '';     });     docId.addEventListener('mousedown', () => docId.select());     docId.addEventListener('click', () => docId.select());   })(); </script>  ## Rate Limiting  The Coda API sets a reasonable limit on the number of requests that can be made per minute. Once this limit is reached, calls to the API will start returning errors with an HTTP status code of 429. If you find yourself hitting rate limits and would like your individual rate to be raised, please contact us at <help+api@coda.io>.  ## Consistency  While edits made in Coda are shared with other collaborators in real-time, it can take a few seconds for them to become available via the API. You may also notice that changes made via the API, such as updating a row, are not immediate. These endpoints all return an HTTP 202 status code, instead of a standard 200, indicating that the edit has been accepted and queued for processing. This generally takes a few seconds, and the edit may fail if invalid. Each such edit will return a `requestId` in the response, and you can pass this `requestId` to the [`#getMutationStatus`](#operation/getMutationStatus) endpoint to find out if it has been applied.  ## Volatile Formulas  Coda exposes a number of \"volatile\" formulas, as as `Today()`, `Now()`, and `User()`. When used in a live Coda doc, these formulas affect what's visible in realtime, tailored to the current user.  Such formulas behave differently with the API. Time-based values may only be current to the last edit made to the doc. User-based values may be blank or invalid.  ## Free and Paid Workspaces  We make the Coda API available to all of our users free of charge, in both free and paid workspaces. However, API usage is subject to the role of the user associated with the API token in the workspace applicable to each API request. What this means is:  - For the [`#createDoc`](#operation/createDoc) endpoint specifically, the owner of the API token must be a Doc   Maker (or Admin) in the workspace. If the \"Any member can create docs\" option in enabled in the workspace   settings, they can be an Editor and will get auto-promoted to Doc Maker upon using this endpoint. Lastly, if in   addition, the API key owner matches the \"Approved email domains\" setting, they will be auto-added to the   workspace and promoted to Doc Maker upon using this endpoint  This behavior applies to the API as well as any integrations that may use it, such as Zapier.  ## Examples  To help you get started, this documentation provides code examples in Python, Unix shell, and Google Apps Script. These examples are based on a simple doc that looks something like this:  ![](https://cdn.coda.io/external/img/api_example_doc.png)  ### Python examples  These examples use Python 3.6+. If you don't already have the `requests` module, use `pip` or `easy_install` to get it.  ### Shell examples  The shell examples are intended to be run in a Unix shell. If you're on Windows, you will need to install [WSL](https://docs.microsoft.com/en-us/windows/wsl/install-win10).  These examples use the standard cURL utility to pull from the API, and then process it with `jq` to extract and format example output. If you don't already have it, you can either [install it](https://stedolan.github.io/jq/) or run the command without it to see the raw JSON output.  ### Google Apps Script examples  ![](https://cdn.coda.io/external/img/api_gas.png)  [Google Apps Script](https://script.google.com/) makes it easy to write code in a JavaScript-like syntax and easily access many Google products with built-in libraries. You can set up your scripts to run periodically, which makes it a good environment for writing tools without maintaining your own server.  Coda provides a library for Google Apps Script. To use it, go into `Resources -> Libraries...` and enter the following library ID: `15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl`. If you want to see the library's source code, it's available [here](https://script.google.com/d/15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl/edit).  Google provides autocomplete for API functions as well as generated docs. You can access these docs via the Libraries dialog by clicking on the library name. Required parameters that would be included in the URL path are positional arguments in each of these functions, followed by the request body, if applicable. All remaining parameters can be specified in the options object.  ## OpenAPI/Swagger Spec  In an effort to standardize our API and make it accessible, we offer an OpenAPI 3.0 specification:  - [OpenAPI 3.0 spec - YAML](https://coda.io/apis/v1/openapi.yaml) - [OpenAPI 3.0 spec - JSON](https://coda.io/apis/v1/openapi.json)  ### Swagger 2.0  We also offer a downgraded Swagger 2.0 version of our specification. This may be useful for a number of tools that haven't yet been adapted to OpenAPI 3.0. Here are the links:  - [Swagger 2.0 spec - YAML](https://coda.io/apis/v1/swagger.yaml) - [Swagger 2.0 spec - JSON](https://coda.io/apis/v1/swagger.json)  #### Postman collection  To get started with prototyping the API quickly in Postman, you can use one of links above to import the Coda API into a collection. You'll then need to set the [appropriate header](#section/Authentication) and environment variables.  ## Client libraries  We do not currently support client libraries apart from Google Apps Script. To work with the Coda API, you can either use standard network libraries for your language, or use the appropriate Swagger Generator tool to auto-generate Coda API client libraries for your language of choice. We do not provide any guarantees that these autogenerated libraries are compatible with our API (e.g., some libraries may not work with Bearer authentication).  ### OpenAPI 3.0  [Swagger Generator 3](https://generator3.swagger.io/) (that link takes you to the docs for the generator API) can generate client libraries for [these languages](https://generator3.swagger.io/v2/clients). It's relatively new and thus only has support for a limited set of languages at this time.  ### Swagger 2.0  [Swagger Generator](https://generator.swagger.io/) takes in a legacy Swagger 2.0 specification, but can generate client libraries for [more languages](http://generator.swagger.io/api/gen/clients). You can also use local [CLI tools](https://swagger.io/docs/open-source-tools/swagger-codegen/) to generate these libraries.  ### Third-party client libraries  Some members of our amazing community have written libraries to work with our API. These aren't officially supported by Coda, but are listed here for convenience. (Please let us know if you've written a library and would like to have it included here.)  - [PHP](https://github.com/danielstieber/CodaPHP) by Daniel Stieber - [Node-RED](https://github.com/serene-water/node-red-contrib-coda-io) by Mori Sugimoto - [NodeJS](https://www.npmjs.com/package/coda-js) by Parker McMullin - [Ruby](https://rubygems.org/gems/coda_docs/) by Carlos Muñoz at Monday.vc - [Python](https://github.com/Blasterai/codaio) by Mikhail Beliansky 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: help+api@coda.io
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// Metadata about a table.
    /// </summary>
    [DataContract]
        public partial class Table :  IEquatable<Table>, IValidatableObject
    {
        /// <summary>
        /// The type of this resource.
        /// </summary>
        /// <value>The type of this resource.</value>
        [JsonConverter(typeof(StringEnumConverter))]
                public enum TypeEnum
        {
            /// <summary>
            /// Enum Table for value: table
            /// </summary>
            [EnumMember(Value = "table")]
            Table = 1        }
        /// <summary>
        /// The type of this resource.
        /// </summary>
        /// <value>The type of this resource.</value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public TypeEnum Type { get; set; }
        /// <summary>
        /// Gets or Sets TableType
        /// </summary>
        [DataMember(Name="tableType", EmitDefaultValue=false)]
        public TableType TableType { get; set; }
        /// <summary>
        /// Gets or Sets Layout
        /// </summary>
        [DataMember(Name="layout", EmitDefaultValue=false)]
        public Layout Layout { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Table" /> class.
        /// </summary>
        /// <param name="id">ID of the table. (required).</param>
        /// <param name="type">The type of this resource. (required).</param>
        /// <param name="tableType">tableType (required).</param>
        /// <param name="href">API link to the table. (required).</param>
        /// <param name="browserLink">Browser-friendly link to the table. (required).</param>
        /// <param name="name">Name of the table. (required).</param>
        /// <param name="parent">parent (required).</param>
        /// <param name="parentTable">parentTable.</param>
        /// <param name="displayColumn">displayColumn (required).</param>
        /// <param name="rowCount">Total number of rows in the table. (required).</param>
        /// <param name="sorts">Any sorts applied to the table. (required).</param>
        /// <param name="layout">layout (required).</param>
        /// <param name="filter">filter.</param>
        /// <param name="createdAt">Timestamp for when the table was created. (required).</param>
        /// <param name="updatedAt">Timestamp for when the table was last modified. (required).</param>
        public Table(string id = default(string), TypeEnum type = default(TypeEnum), TableType tableType = default(TableType), string href = default(string), string browserLink = default(string), string name = default(string), PageReference parent = default(PageReference), TableReference parentTable = default(TableReference), ColumnReference displayColumn = default(ColumnReference), int? rowCount = default(int?), List<Sort> sorts = default(List<Sort>), Layout layout = default(Layout), AllOfTableFilter filter = default(AllOfTableFilter), DateTime? createdAt = default(DateTime?), DateTime? updatedAt = default(DateTime?))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for Table and cannot be null");
            }
            else
            {
                this.Id = id;
            }
            // to ensure "type" is required (not null)
            if (type == null)
            {
                throw new InvalidDataException("type is a required property for Table and cannot be null");
            }
            else
            {
                this.Type = type;
            }
            // to ensure "tableType" is required (not null)
            if (tableType == null)
            {
                throw new InvalidDataException("tableType is a required property for Table and cannot be null");
            }
            else
            {
                this.TableType = tableType;
            }
            // to ensure "href" is required (not null)
            if (href == null)
            {
                throw new InvalidDataException("href is a required property for Table and cannot be null");
            }
            else
            {
                this.Href = href;
            }
            // to ensure "browserLink" is required (not null)
            if (browserLink == null)
            {
                throw new InvalidDataException("browserLink is a required property for Table and cannot be null");
            }
            else
            {
                this.BrowserLink = browserLink;
            }
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for Table and cannot be null");
            }
            else
            {
                this.Name = name;
            }
            // to ensure "parent" is required (not null)
            if (parent == null)
            {
                throw new InvalidDataException("parent is a required property for Table and cannot be null");
            }
            else
            {
                this.Parent = parent;
            }
            // to ensure "displayColumn" is required (not null)
            if (displayColumn == null)
            {
                throw new InvalidDataException("displayColumn is a required property for Table and cannot be null");
            }
            else
            {
                this.DisplayColumn = displayColumn;
            }
            // to ensure "rowCount" is required (not null)
            if (rowCount == null)
            {
                throw new InvalidDataException("rowCount is a required property for Table and cannot be null");
            }
            else
            {
                this.RowCount = rowCount;
            }
            // to ensure "sorts" is required (not null)
            if (sorts == null)
            {
                throw new InvalidDataException("sorts is a required property for Table and cannot be null");
            }
            else
            {
                this.Sorts = sorts;
            }
            // to ensure "layout" is required (not null)
            if (layout == null)
            {
                throw new InvalidDataException("layout is a required property for Table and cannot be null");
            }
            else
            {
                this.Layout = layout;
            }
            // to ensure "createdAt" is required (not null)
            if (createdAt == null)
            {
                throw new InvalidDataException("createdAt is a required property for Table and cannot be null");
            }
            else
            {
                this.CreatedAt = createdAt;
            }
            // to ensure "updatedAt" is required (not null)
            if (updatedAt == null)
            {
                throw new InvalidDataException("updatedAt is a required property for Table and cannot be null");
            }
            else
            {
                this.UpdatedAt = updatedAt;
            }
            this.ParentTable = parentTable;
            this.Filter = filter;
        }
        
        /// <summary>
        /// ID of the table.
        /// </summary>
        /// <value>ID of the table.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }



        /// <summary>
        /// API link to the table.
        /// </summary>
        /// <value>API link to the table.</value>
        [DataMember(Name="href", EmitDefaultValue=false)]
        public string Href { get; set; }

        /// <summary>
        /// Browser-friendly link to the table.
        /// </summary>
        /// <value>Browser-friendly link to the table.</value>
        [DataMember(Name="browserLink", EmitDefaultValue=false)]
        public string BrowserLink { get; set; }

        /// <summary>
        /// Name of the table.
        /// </summary>
        /// <value>Name of the table.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Parent
        /// </summary>
        [DataMember(Name="parent", EmitDefaultValue=false)]
        public PageReference Parent { get; set; }

        /// <summary>
        /// Gets or Sets ParentTable
        /// </summary>
        [DataMember(Name="parentTable", EmitDefaultValue=false)]
        public TableReference ParentTable { get; set; }

        /// <summary>
        /// Gets or Sets DisplayColumn
        /// </summary>
        [DataMember(Name="displayColumn", EmitDefaultValue=false)]
        public ColumnReference DisplayColumn { get; set; }

        /// <summary>
        /// Total number of rows in the table.
        /// </summary>
        /// <value>Total number of rows in the table.</value>
        [DataMember(Name="rowCount", EmitDefaultValue=false)]
        public int? RowCount { get; set; }

        /// <summary>
        /// Any sorts applied to the table.
        /// </summary>
        /// <value>Any sorts applied to the table.</value>
        [DataMember(Name="sorts", EmitDefaultValue=false)]
        public List<Sort> Sorts { get; set; }


        /// <summary>
        /// Gets or Sets Filter
        /// </summary>
        [DataMember(Name="filter", EmitDefaultValue=false)]
        public AllOfTableFilter Filter { get; set; }

        /// <summary>
        /// Timestamp for when the table was created.
        /// </summary>
        /// <value>Timestamp for when the table was created.</value>
        [DataMember(Name="createdAt", EmitDefaultValue=false)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Timestamp for when the table was last modified.
        /// </summary>
        /// <value>Timestamp for when the table was last modified.</value>
        [DataMember(Name="updatedAt", EmitDefaultValue=false)]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Table {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  TableType: ").Append(TableType).Append("\n");
            sb.Append("  Href: ").Append(Href).Append("\n");
            sb.Append("  BrowserLink: ").Append(BrowserLink).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Parent: ").Append(Parent).Append("\n");
            sb.Append("  ParentTable: ").Append(ParentTable).Append("\n");
            sb.Append("  DisplayColumn: ").Append(DisplayColumn).Append("\n");
            sb.Append("  RowCount: ").Append(RowCount).Append("\n");
            sb.Append("  Sorts: ").Append(Sorts).Append("\n");
            sb.Append("  Layout: ").Append(Layout).Append("\n");
            sb.Append("  Filter: ").Append(Filter).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Table);
        }

        /// <summary>
        /// Returns true if Table instances are equal
        /// </summary>
        /// <param name="input">Instance of Table to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Table input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.TableType == input.TableType ||
                    (this.TableType != null &&
                    this.TableType.Equals(input.TableType))
                ) && 
                (
                    this.Href == input.Href ||
                    (this.Href != null &&
                    this.Href.Equals(input.Href))
                ) && 
                (
                    this.BrowserLink == input.BrowserLink ||
                    (this.BrowserLink != null &&
                    this.BrowserLink.Equals(input.BrowserLink))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Parent == input.Parent ||
                    (this.Parent != null &&
                    this.Parent.Equals(input.Parent))
                ) && 
                (
                    this.ParentTable == input.ParentTable ||
                    (this.ParentTable != null &&
                    this.ParentTable.Equals(input.ParentTable))
                ) && 
                (
                    this.DisplayColumn == input.DisplayColumn ||
                    (this.DisplayColumn != null &&
                    this.DisplayColumn.Equals(input.DisplayColumn))
                ) && 
                (
                    this.RowCount == input.RowCount ||
                    (this.RowCount != null &&
                    this.RowCount.Equals(input.RowCount))
                ) && 
                (
                    this.Sorts == input.Sorts ||
                    this.Sorts != null &&
                    input.Sorts != null &&
                    this.Sorts.SequenceEqual(input.Sorts)
                ) && 
                (
                    this.Layout == input.Layout ||
                    (this.Layout != null &&
                    this.Layout.Equals(input.Layout))
                ) && 
                (
                    this.Filter == input.Filter ||
                    (this.Filter != null &&
                    this.Filter.Equals(input.Filter))
                ) && 
                (
                    this.CreatedAt == input.CreatedAt ||
                    (this.CreatedAt != null &&
                    this.CreatedAt.Equals(input.CreatedAt))
                ) && 
                (
                    this.UpdatedAt == input.UpdatedAt ||
                    (this.UpdatedAt != null &&
                    this.UpdatedAt.Equals(input.UpdatedAt))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.TableType != null)
                    hashCode = hashCode * 59 + this.TableType.GetHashCode();
                if (this.Href != null)
                    hashCode = hashCode * 59 + this.Href.GetHashCode();
                if (this.BrowserLink != null)
                    hashCode = hashCode * 59 + this.BrowserLink.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Parent != null)
                    hashCode = hashCode * 59 + this.Parent.GetHashCode();
                if (this.ParentTable != null)
                    hashCode = hashCode * 59 + this.ParentTable.GetHashCode();
                if (this.DisplayColumn != null)
                    hashCode = hashCode * 59 + this.DisplayColumn.GetHashCode();
                if (this.RowCount != null)
                    hashCode = hashCode * 59 + this.RowCount.GetHashCode();
                if (this.Sorts != null)
                    hashCode = hashCode * 59 + this.Sorts.GetHashCode();
                if (this.Layout != null)
                    hashCode = hashCode * 59 + this.Layout.GetHashCode();
                if (this.Filter != null)
                    hashCode = hashCode * 59 + this.Filter.GetHashCode();
                if (this.CreatedAt != null)
                    hashCode = hashCode * 59 + this.CreatedAt.GetHashCode();
                if (this.UpdatedAt != null)
                    hashCode = hashCode * 59 + this.UpdatedAt.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
