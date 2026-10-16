using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace VS_Theme_Editor;

public partial class CategoryData : ObservableObject, Json.IDictionaryable
{
    [property: JsonIgnore]
    [ObservableProperty]
    private Guid guid;

    [JsonIgnore]
    public string? Id {
        get => Guid.ToString();
        set => Guid = new Guid(value!);
    }

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private Int32 dataLength;

    [ObservableProperty]
    private Int32 headerLength;

    [ObservableProperty]
    private Int32 categoryCount;

    [ObservableProperty]
    private Int32 entryCount;

    [ObservableProperty]
    private List<ThemeColorEntry> entries = new List<ThemeColorEntry>();

}