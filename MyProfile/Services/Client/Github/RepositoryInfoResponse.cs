namespace MyProfile.Services.Client.Github;


/// <summary>
///  Это модель данных ответа из парсинга Readme.md файла репозитория
///  Данные достаются по ключу id из кода HTML элемента
/// </summary>
public class RepositoryInfoResponse
{
    /// <summary> Repository Name - сохраняем, чтобы сопоставить данные с другой информацией о репо </summary>
    public string Name { get; set; }
    
    /// <summary> Main Title - Заголовок проекта </summary>
    public string MainTitle { get; set; }
    
    /// <summary> Sub Title - Краткое описание проекта </summary>
    public string SubTitle { get; set; }
    
    /// <summary> Список технологий примененных в проекте </summary>
    public string[] Technologies { get; set; }
}