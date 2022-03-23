using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DipSampleUploadButton.Repository
{
    public class JsonProperty
    {
        public Permissions permissions { get; set; }
        public Processgroupflow processGroupFlow { get; set; }
    }

    public class Permissions
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Processgroupflow
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string parentGroupId { get; set; }
        public Breadcrumb breadcrumb { get; set; }
        public Flow flow { get; set; }
        public string lastRefreshed { get; set; }
    }

    public class Breadcrumb
    {
        public string id { get; set; }
        public Permissions1 permissions { get; set; }
        public Breadcrumb1 breadcrumb { get; set; }
        public Parentbreadcrumb parentBreadcrumb { get; set; }
    }

    public class Permissions1
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Breadcrumb1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Parentbreadcrumb
    {
        public string id { get; set; }
        public Permissions2 permissions { get; set; }
        public Breadcrumb2 breadcrumb { get; set; }
    }

    public class Permissions2
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Breadcrumb2
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Flow
    {
        public object[] processGroups { get; set; }
        public object[] remoteProcessGroups { get; set; }
        public Processor[] processors { get; set; }
        public object[] inputPorts { get; set; }
        public object[] outputPorts { get; set; }
        public Connection[] connections { get; set; }
        public object[] labels { get; set; }
        public Funnel[] funnels { get; set; }
    }

    public class Processor
    {
        public Revision revision { get; set; }
        public string id { get; set; }
        public string uri { get; set; }
        public Position position { get; set; }
        public Permissions3 permissions { get; set; }
        public object[] bulletins { get; set; }
        public Component component { get; set; }
        public string inputRequirement { get; set; }
        public Status status { get; set; }
        public Operatepermissions operatePermissions { get; set; }
    }

    public class Revision
    {
        public string clientId { get; set; }
        public int version { get; set; }
    }

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Permissions3
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Component
    {
        public string id { get; set; }
        public string parentGroupId { get; set; }
        public Position1 position { get; set; }
        public string state { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Bundle bundle { get; set; }
        public Style style { get; set; }
        public Relationship[] relationships { get; set; }
        public bool supportsParallelProcessing { get; set; }
        public bool supportsEventDriven { get; set; }
        public bool supportsBatching { get; set; }
        public bool persistsState { get; set; }
        public bool restricted { get; set; }
        public bool deprecated { get; set; }
        public bool executionNodeRestricted { get; set; }
        public bool multipleVersionsAvailable { get; set; }
        public string inputRequirement { get; set; }
        public Config config { get; set; }
        public string validationStatus { get; set; }
        public bool extensionMissing { get; set; }
    }

    public class Position1
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Bundle
    {
        public string group { get; set; }
        public string artifact { get; set; }
        public string version { get; set; }
    }

    public class Style
    {
    }

    public class Config
    {
        public Properties properties { get; set; }
        public Descriptors descriptors { get; set; }
        public string schedulingPeriod { get; set; }
        public string schedulingStrategy { get; set; }
        public string executionNode { get; set; }
        public string penaltyDuration { get; set; }
        public string yieldDuration { get; set; }
        public string bulletinLevel { get; set; }
        public int runDurationMillis { get; set; }
        public int concurrentlySchedulableTaskCount { get; set; }
        public string comments { get; set; }
        public bool lossTolerant { get; set; }
        public Defaultconcurrenttasks defaultConcurrentTasks { get; set; }
        public Defaultschedulingperiod defaultSchedulingPeriod { get; set; }
    }

    public class Properties
    {
        public string InputDirectory { get; set; }
        public string FileFilter { get; set; }
        public object PathFilter { get; set; }
        public string BatchSize { get; set; }
        public string KeepSourceFile { get; set; }
        public string RecurseSubdirectories { get; set; }
        public string PollingInterval { get; set; }
        public string IgnoreHiddenFiles { get; set; }
        public string MinimumFileAge { get; set; }
        public object MaximumFileAge { get; set; }
        public string MinimumFileSize { get; set; }
        public object MaximumFileSize { get; set; }

        public string putdbrecordrecordreader { get; set; }
        public string putdbrecordstatementtype { get; set; }
        public string putdbrecorddcbpservice { get; set; }
        public object putdbrecordcatalogname { get; set; }
        public object putdbrecordschemaname { get; set; }
        public string putdbrecordtablename { get; set; }
        public string putdbrecordtranslatefieldnames { get; set; }
        public string putdbrecordunmatchedfieldbehavior { get; set; }
        public string putdbrecordunmatchedcolumnbehavior { get; set; }
        public object putdbrecordupdatekeys { get; set; }
        public object putdbrecordfieldcontainingsql { get; set; }
        public string putdbrecordquotedidentifiers { get; set; }
        public string putdbrecordquotedtableidentifiers { get; set; }
        public string putdbrecordquerytimeout { get; set; }
        public string rollbackonfailure { get; set; }
        public string tableschemacachesize { get; set; }
        public string putdbrecordmaxbatchsize { get; set; }
        public object extractsheets { get; set; }
        public string excelextractfirstrow { get; set; }
        public object excelextractcolumntoskip { get; set; }
        public string excelformatvalues { get; set; }
        public string CSVFormat { get; set; }
        public string ValueSeparator { get; set; }
        public string IncludeHeaderLine { get; set; }
        public string QuoteCharacter { get; set; }
        public string EscapeCharacter { get; set; }
        public object CommentMarker { get; set; }
        public object NullString { get; set; }
        public string TrimFields { get; set; }
        public string QuoteMode { get; set; }
        public string RecordSeparator { get; set; }
        public string IncludeTrailingDelimiter { get; set; }
    }

    public class Descriptors
    {
        public InputDirectory InputDirectory { get; set; }
        public FileFilter FileFilter { get; set; }
        public PathFilter PathFilter { get; set; }
        public BatchSize BatchSize { get; set; }
        public KeepSourceFile KeepSourceFile { get; set; }
        public RecurseSubdirectories RecurseSubdirectories { get; set; }
        public PollingInterval PollingInterval { get; set; }
        public IgnoreHiddenFiles IgnoreHiddenFiles { get; set; }
        public MinimumFileAge MinimumFileAge { get; set; }
        public MaximumFileAge MaximumFileAge { get; set; }
        public MinimumFileSize MinimumFileSize { get; set; }
        public MaximumFileSize MaximumFileSize { get; set; }
        [JsonProperty("put-db-record-record-reader")]
        public PutDbRecordRecordReader putdbrecordrecordreader { get; set; }
        public PutDbRecordStatementType putdbrecordstatementtype { get; set; }
        [JsonProperty("put-db-record-dcbp-service")]
        public PutDbRecordDcbpService putdbrecorddcbpservice { get; set; }
        public PutDbRecordCatalogName putdbrecordcatalogname { get; set; }
        public PutDbRecordSchemaName putdbrecordschemaname { get; set; }
        public PutDbRecordTableName putdbrecordtablename { get; set; }
        public PutDbRecordTranslateFieldNames putdbrecordtranslatefieldnames { get; set; }
        public PutDbRecordUnmatchedFieldBehavior putdbrecordunmatchedfieldbehavior { get; set; }
        public PutDbRecordUnmatchedColumnBehavior putdbrecordunmatchedcolumnbehavior { get; set; }
        public PutDbRecordUpdateKeys putdbrecordupdatekeys { get; set; }
        public PutDbRecordFieldContainingSql putdbrecordfieldcontainingsql { get; set; }
        public PutDbRecordQuotedIdentifiers putdbrecordquotedidentifiers { get; set; }
        public PutDbRecordQuotedTableIdentifiers putdbrecordquotedtableidentifiers { get; set; }
        public PutDbRecordQueryTimeout putdbrecordquerytimeout { get; set; }
        public RollbackOnFailure rollbackonfailure { get; set; }
        public TableSchemaCacheSize tableschemacachesize { get; set; }
        public PutDbRecordMaxBatchSize putdbrecordmaxbatchsize { get; set; }
        public ExtractSheets extractsheets { get; set; }
        public ExcelExtractFirstRow excelextractfirstrow { get; set; }
        public ExcelExtractColumnToSkip excelextractcolumntoskip { get; set; }
        public ExcelFormatValues excelformatvalues { get; set; }
        public CSVFormat CSVFormat { get; set; }
        public ValueSeparator ValueSeparator { get; set; }
        public IncludeHeaderLine IncludeHeaderLine { get; set; }
        public QuoteCharacter QuoteCharacter { get; set; }
        public EscapeCharacter EscapeCharacter { get; set; }
        public CommentMarker CommentMarker { get; set; }
        public NullString NullString { get; set; }
        public TrimFields TrimFields { get; set; }
        public QuoteMode QuoteMode { get; set; }
        public RecordSeparator RecordSeparator { get; set; }
        public IncludeTrailingDelimiter IncludeTrailingDelimiter { get; set; }
    }

    public class InputDirectory
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class FileFilter
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PathFilter
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class BatchSize
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class KeepSourceFile
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue
    {
        public Allowablevalue1 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue1
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class RecurseSubdirectories
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue2[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue2
    {
        public Allowablevalue3 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue3
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PollingInterval
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class IgnoreHiddenFiles
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue4[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue4
    {
        public Allowablevalue5 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue5
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class MinimumFileAge
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class MaximumFileAge
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class MinimumFileSize
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class MaximumFileSize
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordRecordReader
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public Allowablevalue6[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
        public string identifiesControllerService { get; set; }
        public Identifiescontrollerservicebundle identifiesControllerServiceBundle { get; set; }
    }

    public class Identifiescontrollerservicebundle
    {
        public string group { get; set; }
        public string artifact { get; set; }
        public string version { get; set; }
    }

    public class Allowablevalue6
    {
        public Allowablevalue7 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue7
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordStatementType
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public Allowablevalue8[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue8
    {
        public Allowablevalue9 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue9
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordDcbpService
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public Allowablevalue10[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
        public string identifiesControllerService { get; set; }
        public Identifiescontrollerservicebundle1 identifiesControllerServiceBundle { get; set; }
    }

    public class Identifiescontrollerservicebundle1
    {
        public string group { get; set; }
        public string artifact { get; set; }
        public string version { get; set; }
    }

    public class Allowablevalue10
    {
        public Allowablevalue11 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue11
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordCatalogName
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordSchemaName
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordTableName
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordTranslateFieldNames
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue12[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue12
    {
        public Allowablevalue13 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue13
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordUnmatchedFieldBehavior
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue14[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue14
    {
        public Allowablevalue15 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue15
    {
        public string displayName { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }

    public class PutDbRecordUnmatchedColumnBehavior
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue16[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue16
    {
        public Allowablevalue17 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue17
    {
        public string displayName { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }

    public class PutDbRecordUpdateKeys
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordFieldContainingSql
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordQuotedIdentifiers
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue18[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue18
    {
        public Allowablevalue19 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue19
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordQuotedTableIdentifiers
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue20[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue20
    {
        public Allowablevalue21 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue21
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class PutDbRecordQueryTimeout
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class RollbackOnFailure
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue22[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue22
    {
        public Allowablevalue23 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue23
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class TableSchemaCacheSize
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class PutDbRecordMaxBatchSize
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class ExtractSheets
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class ExcelExtractFirstRow
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class ExcelExtractColumnToSkip
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class ExcelFormatValues
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue24[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue24
    {
        public Allowablevalue25 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue25
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class CSVFormat
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue26[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue26
    {
        public Allowablevalue27 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue27
    {
        public string displayName { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }

    public class ValueSeparator
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class IncludeHeaderLine
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue28[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue28
    {
        public Allowablevalue29 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue29
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class QuoteCharacter
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class EscapeCharacter
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class CommentMarker
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class NullString
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class TrimFields
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue30[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue30
    {
        public Allowablevalue31 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue31
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class QuoteMode
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue32[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue32
    {
        public Allowablevalue33 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue33
    {
        public string displayName { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }

    public class RecordSeparator
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class IncludeTrailingDelimiter
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public Allowablevalue34[] allowableValues { get; set; }
        public bool required { get; set; }
        public bool sensitive { get; set; }
        public bool dynamic { get; set; }
        public bool supportsEl { get; set; }
        public string expressionLanguageScope { get; set; }
    }

    public class Allowablevalue34
    {
        public Allowablevalue35 allowableValue { get; set; }
        public bool canRead { get; set; }
    }

    public class Allowablevalue35
    {
        public string displayName { get; set; }
        public string value { get; set; }
    }

    public class Defaultconcurrenttasks
    {
        public string TIMER_DRIVEN { get; set; }
        public string EVENT_DRIVEN { get; set; }
        public string RUN_ONCE { get; set; }
        public string CRON_DRIVEN { get; set; }
    }

    public class Defaultschedulingperiod
    {
        public string TIMER_DRIVEN { get; set; }
        public string RUN_ONCE { get; set; }
        public string CRON_DRIVEN { get; set; }
    }

    public class Relationship
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool autoTerminate { get; set; }
    }

    public class Status
    {
        public string groupId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string runStatus { get; set; }
        public string statsLastRefreshed { get; set; }
        public Aggregatesnapshot aggregateSnapshot { get; set; }
    }

    public class Aggregatesnapshot
    {
        public string id { get; set; }
        public string groupId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string runStatus { get; set; }
        public string executionNode { get; set; }
        public int bytesRead { get; set; }
        public int bytesWritten { get; set; }
        public string read { get; set; }
        public string written { get; set; }
        public int flowFilesIn { get; set; }
        public int bytesIn { get; set; }
        public string input { get; set; }
        public int flowFilesOut { get; set; }
        public int bytesOut { get; set; }
        public string output { get; set; }
        public int taskCount { get; set; }
        public int tasksDurationNanos { get; set; }
        public string tasks { get; set; }
        public string tasksDuration { get; set; }
        public int activeThreadCount { get; set; }
        public int terminatedThreadCount { get; set; }
    }

    public class Operatepermissions
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Connection
    {
        public Revision1 revision { get; set; }
        public string id { get; set; }
        public string uri { get; set; }
        public Permissions4 permissions { get; set; }
        public Component1 component { get; set; }
        public Status1 status { get; set; }
        public object[] bends { get; set; }
        public int labelIndex { get; set; }
        public int zIndex { get; set; }
        public string sourceId { get; set; }
        public string sourceGroupId { get; set; }
        public string sourceType { get; set; }
        public string destinationId { get; set; }
        public string destinationGroupId { get; set; }
        public string destinationType { get; set; }
    }

    public class Revision1
    {
        public string clientId { get; set; }
        public int version { get; set; }
    }

    public class Permissions4
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Component1
    {
        public string id { get; set; }
        public string parentGroupId { get; set; }
        public Source source { get; set; }
        public Destination destination { get; set; }
        public string name { get; set; }
        public int labelIndex { get; set; }
        public int zIndex { get; set; }
        public string[] selectedRelationships { get; set; }
        public string[] availableRelationships { get; set; }
        public int backPressureObjectThreshold { get; set; }
        public string backPressureDataSizeThreshold { get; set; }
        public string flowFileExpiration { get; set; }
        public object[] prioritizers { get; set; }
        public object[] bends { get; set; }
        public string loadBalanceStrategy { get; set; }
        public string loadBalancePartitionAttribute { get; set; }
        public string loadBalanceCompression { get; set; }
        public string loadBalanceStatus { get; set; }
    }

    public class Source
    {
        public string id { get; set; }
        public string type { get; set; }
        public string groupId { get; set; }
        public string name { get; set; }
        public bool running { get; set; }
        public string comments { get; set; }
    }

    public class Destination
    {
        public string id { get; set; }
        public string type { get; set; }
        public string groupId { get; set; }
        public string name { get; set; }
        public bool running { get; set; }
        public string comments { get; set; }
    }

    public class Status1
    {
        public string id { get; set; }
        public string groupId { get; set; }
        public string name { get; set; }
        public string statsLastRefreshed { get; set; }
        public string sourceId { get; set; }
        public string sourceName { get; set; }
        public string destinationId { get; set; }
        public string destinationName { get; set; }
        public Aggregatesnapshot1 aggregateSnapshot { get; set; }
    }

    public class Aggregatesnapshot1
    {
        public string id { get; set; }
        public string groupId { get; set; }
        public string name { get; set; }
        public string sourceName { get; set; }
        public string destinationName { get; set; }
        public int flowFilesIn { get; set; }
        public int bytesIn { get; set; }
        public string input { get; set; }
        public int flowFilesOut { get; set; }
        public int bytesOut { get; set; }
        public string output { get; set; }
        public int flowFilesQueued { get; set; }
        public int bytesQueued { get; set; }
        public string queued { get; set; }
        public string queuedSize { get; set; }
        public string queuedCount { get; set; }
        public int percentUseCount { get; set; }
        public int percentUseBytes { get; set; }
    }

    public class Funnel
    {
        public Revision2 revision { get; set; }
        public string id { get; set; }
        public string uri { get; set; }
        public Position2 position { get; set; }
        public Permissions5 permissions { get; set; }
        public Component2 component { get; set; }
    }

    public class Revision2
    {
        public string clientId { get; set; }
        public int version { get; set; }
    }

    public class Position2
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class Permissions5
    {
        public bool canRead { get; set; }
        public bool canWrite { get; set; }
    }

    public class Component2
    {
        public string id { get; set; }
        public string parentGroupId { get; set; }
        public Position3 position { get; set; }
    }

    public class Position3
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}
