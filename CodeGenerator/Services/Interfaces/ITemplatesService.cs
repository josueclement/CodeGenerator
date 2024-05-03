using System.Collections.ObjectModel;
using CodeGenerator.Model;

namespace CodeGenerator.Services.Interfaces;

public interface ITemplatesService
{
    ObservableCollection<CodeTemplate> Templates { get; set; }
    string RepositoryFilePath { get; set; }
}