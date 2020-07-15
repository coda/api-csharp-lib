# IO.Swagger.Model.Table
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | ID of the table. | 
**Type** | **string** | The type of this resource. | 
**TableType** | **TableType** |  | 
**Href** | **string** | API link to the table. | 
**BrowserLink** | **string** | Browser-friendly link to the table. | 
**Name** | **string** | Name of the table. | 
**Parent** | [**PageReference**](PageReference.md) |  | 
**ParentTable** | [**TableReference**](TableReference.md) |  | [optional] 
**DisplayColumn** | [**ColumnReference**](ColumnReference.md) |  | 
**RowCount** | **int?** | Total number of rows in the table. | 
**Sorts** | [**List&lt;Sort&gt;**](Sort.md) | Any sorts applied to the table. | 
**Layout** | **Layout** |  | 
**Filter** | [**AllOfTableFilter**](AllOfTableFilter.md) |  | [optional] 
**CreatedAt** | **DateTime?** | Timestamp for when the table was created. | 
**UpdatedAt** | **DateTime?** | Timestamp for when the table was last modified. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

