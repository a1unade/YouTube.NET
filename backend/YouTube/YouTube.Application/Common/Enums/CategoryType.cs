using System.ComponentModel;

namespace YouTube.Application.Common.Enums;

public enum CategoryType
{
    [Description("Юмор")]
    Humor = 1,
    
    [Description("Спорт")]
    Sport = 2,
    
    [Description("Игры")]
    Games = 3,
    
    [Description("Музыка")]
    Music = 4,
    
    [Description("Развлечения")]
    Entertainment = 5,
    
    [Description("Фильмы и анимации")]
    MoviesAndAnimations = 6,
    
    [Description("Наука")]
    Science = 7
}