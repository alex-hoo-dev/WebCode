namespace WebCodeCli.Helpers;

/// <summary>
/// 文件图标帮助类 - 统一管理文件类型图标
/// </summary>
public static class FileIconHelper
{
    /// <summary>
    /// 获取文件图标 SVG (小尺寸 - 用于文件树)
    /// </summary>
    /// <param name="extension">文件扩展名 (如 ".doc", ".pdf")</param>
    /// <returns>SVG HTML 字符串</returns>
    public static string GetFileIcon(string? extension)
    {
        if (string.IsNullOrEmpty(extension))
            return GetDefaultIcon();
            
        return extension.ToLower() switch
        {
            // Office - Word (蓝色)
            ".doc" or ".docx" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-blue-600 group-hover:text-blue-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4zm2 6a1 1 0 011-1h6a1 1 0 110 2H7a1 1 0 01-1-1zm1 3a1 1 0 100 2h6a1 1 0 100-2H7z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Office - Excel (绿色)
            ".xls" or ".xlsx" or ".xlsm" or ".csv" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-green-600 group-hover:text-green-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M3 4a1 1 0 011-1h12a1 1 0 011 1v2a1 1 0 01-1 1H4a1 1 0 01-1-1V4zM3 10a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H4a1 1 0 01-1-1v-6zM14 9a1 1 0 00-1 1v6a1 1 0 001 1h2a1 1 0 001-1v-6a1 1 0 00-1-1h-2z\"></path></svg>",
            
            // Office - PowerPoint (橙色)
            ".ppt" or ".pptx" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-orange-600 group-hover:text-orange-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm3 2h6v4H7V5zm8 8v2h1v-2h-1zm-2-3h2v2h-2v-2zm2-3h-2V5h2v2zm-4 3H7v2h4v-2z\"></path></svg>",
            
            // PDF (红色)
            ".pdf" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-red-600 group-hover:text-red-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Images (粉色)
            ".png" or ".jpg" or ".jpeg" or ".gif" or ".webp" or ".ico" or ".bmp" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-pink-500 group-hover:text-pink-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z\" clip-rule=\"evenodd\"></path></svg>",
            
            // SVG (紫色)
            ".svg" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-purple-500 group-hover:text-purple-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Archive (黄色)
            ".zip" or ".rar" or ".7z" or ".tar" or ".gz" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-yellow-600 group-hover:text-yellow-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path d=\"M2 6a2 2 0 012-2h5l2 2h5a2 2 0 012 2v6a2 2 0 01-2 2H4a2 2 0 01-2-2V6z\"></path></svg>",
            
            // JSON (绿色)
            ".json" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-green-500 group-hover:text-green-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M3 4a1 1 0 011-1h4a1 1 0 010 2H6.414l2.293 2.293a1 1 0 11-1.414 1.414L5 6.414V8a1 1 0 01-2 0V4zm9 1a1 1 0 010-2h4a1 1 0 011 1v4a1 1 0 01-2 0V6.414l-2.293 2.293a1 1 0 11-1.414-1.414L13.586 5H12zm-9 7a1 1 0 012 0v1.586l2.293-2.293a1 1 0 111.414 1.414L6.414 15H8a1 1 0 010 2H4a1 1 0 01-1-1v-4zm13-1a1 1 0 011 1v4a1 1 0 01-1 1h-4a1 1 0 010-2h1.586l-2.293-2.293a1 1 0 111.414-1.414L15 13.586V12a1 1 0 011-1z\" clip-rule=\"evenodd\"></path></svg>",
            
            // HTML (橙色)
            ".html" or ".htm" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-orange-500 group-hover:text-orange-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path d=\"M3 4a1 1 0 011-1h12a1 1 0 011 1v2a1 1 0 01-1 1H4a1 1 0 01-1-1V4zM3 10a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H4a1 1 0 01-1-1v-6zM14 9a1 1 0 00-1 1v6a1 1 0 001 1h2a1 1 0 001-1v-6a1 1 0 00-1-1h-2z\"></path></svg>",
            
            // CSS (蓝色)
            ".css" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-blue-500 group-hover:text-blue-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // JavaScript (黄色)
            ".js" or ".jsx" or ".mjs" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-yellow-500 group-hover:text-yellow-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // TypeScript (蓝色)
            ".ts" or ".tsx" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-blue-600 group-hover:text-blue-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Python (蓝色)
            ".py" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-blue-500 group-hover:text-blue-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // C# (紫色)
            ".cs" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-purple-600 group-hover:text-purple-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Java (红色)
            ".java" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-red-600 group-hover:text-red-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Razor (紫色)
            ".razor" or ".cshtml" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-purple-600 group-hover:text-purple-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path d=\"M3 4a1 1 0 011-1h12a1 1 0 011 1v2a1 1 0 01-1 1H4a1 1 0 01-1-1V4zM3 10a1 1 0 011-1h6a1 1 0 011 1v6a1 1 0 01-1 1H4a1 1 0 01-1-1v-6zM14 9a1 1 0 00-1 1v6a1 1 0 001 1h2a1 1 0 001-1v-6a1 1 0 00-1-1h-2z\"></path></svg>",
            
            // XML (橙色)
            ".xml" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-orange-600 group-hover:text-orange-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M12.316 3.051a1 1 0 01.633 1.265l-4 12a1 1 0 11-1.898-.632l4-12a1 1 0 011.265-.633zM5.707 6.293a1 1 0 010 1.414L3.414 10l2.293 2.293a1 1 0 11-1.414 1.414l-3-3a1 1 0 010-1.414l3-3a1 1 0 011.414 0zm8.586 0a1 1 0 011.414 0l3 3a1 1 0 010 1.414l-3 3a1 1 0 11-1.414-1.414L16.586 10l-2.293-2.293a1 1 0 010-1.414z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Markdown (灰色)
            ".md" or ".markdown" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-gray-600 group-hover:text-gray-700 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4zm2 6a1 1 0 011-1h6a1 1 0 110 2H7a1 1 0 01-1-1zm1 3a1 1 0 100 2h6a1 1 0 100-2H7z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Text files (灰色)
            ".txt" or ".log" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-gray-500 group-hover:text-gray-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 4a2 2 0 012-2h4.586A2 2 0 0112 2.586L15.414 6A2 2 0 0116 7.414V16a2 2 0 01-2 2H6a2 2 0 01-2-2V4zm2 6a1 1 0 011-1h6a1 1 0 110 2H7a1 1 0 01-1-1zm1 3a1 1 0 100 2h6a1 1 0 100-2H7z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Config files (青色)
            ".config" or ".ini" or ".env" or ".toml" or ".yaml" or ".yml" => "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-teal-500 group-hover:text-teal-600 flex-shrink-0 transition-colors\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M11.49 3.17c-.38-1.56-2.6-1.56-2.98 0a1.532 1.532 0 01-2.286.948c-1.372-.836-2.942.734-2.106 2.106.54.886.061 2.042-.947 2.287-1.561.379-1.561 2.6 0 2.978a1.532 1.532 0 01.947 2.287c-.836 1.372.734 2.942 2.106 2.106a1.532 1.532 0 012.287.947c.379 1.561 2.6 1.561 2.978 0a1.533 1.533 0 012.287-.947c1.372.836 2.942-.734 2.106-2.106a1.533 1.533 0 01.947-2.287c1.561-.379 1.561-2.6 0-2.978a1.532 1.532 0 01-.947-2.287c.836-1.372-.734-2.942-2.106-2.106a1.532 1.532 0 01-2.287-.947zM10 13a3 3 0 100-6 3 3 0 000 6z\" clip-rule=\"evenodd\"></path></svg>",
            
            // Default file icon
            _ => GetDefaultIcon()
        };
    }

    /// <summary>
    /// 获取文件图标 SVG (大尺寸 - 用于预览模态框等)
    /// </summary>
    /// <param name="extension">文件扩展名 (如 ".doc", ".pdf")</param>
    /// <returns>SVG HTML 字符串</returns>
    public static string GetLargeFileIcon(string? extension)
    {
        if (string.IsNullOrEmpty(extension))
            return GetLargeDefaultIcon();
            
        return extension.ToLower() switch
        {
            // Word
            ".doc" or ".docx" or ".docm" or ".dotx" or ".dotm" or ".odt" =>
                "<svg class=\"w-6 h-6 text-blue-600 flex-shrink-0\" fill=\"currentColor\" viewBox=\"0 0 24 24\"><path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8l-6-6zM9.5 18.5L6 8h2l2 6 2-6h2l-3.5 10.5h-1zM14 9V3.5L19.5 9H14z\"/></svg>",
            
            // Excel
            ".xls" or ".xlsx" or ".xlsm" or ".xlsb" or ".xltx" or ".xltm" or ".csv" or ".ods" =>
                "<svg class=\"w-6 h-6 text-green-600 flex-shrink-0\" fill=\"currentColor\" viewBox=\"0 0 24 24\"><path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8l-6-6zM9 19H7v-2h2v2zm0-4H7v-2h2v2zm4 4h-2v-2h2v2zm0-4h-2v-2h2v2zm4 4h-2v-2h2v2zm0-4h-2v-2h2v2zM14 9V3.5L19.5 9H14z\"/></svg>",
            
            // PowerPoint
            ".ppt" or ".pptx" or ".pptm" or ".potx" or ".potm" or ".ppsx" or ".ppsm" or ".odp" =>
                "<svg class=\"w-6 h-6 text-orange-600 flex-shrink-0\" fill=\"currentColor\" viewBox=\"0 0 24 24\"><path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8l-6-6zM9 19H7V9h4c1.1 0 2 .9 2 2v2c0 1.1-.9 2-2 2H9v4zm0-6h2v-2H9v2zM14 9V3.5L19.5 9H14z\"/></svg>",
            
            // PDF
            ".pdf" =>
                "<svg class=\"w-6 h-6 text-red-500 flex-shrink-0\" fill=\"currentColor\" viewBox=\"0 0 24 24\"><path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8l-6-6zM14 9V3.5L19.5 9H14z\"/></svg>",
            
            // 图片
            ".png" or ".jpg" or ".jpeg" or ".gif" or ".webp" or ".bmp" or ".svg" or ".ico" =>
                "<svg class=\"w-6 h-6 text-pink-500 flex-shrink-0\" fill=\"currentColor\" viewBox=\"0 0 20 20\"><path fill-rule=\"evenodd\" d=\"M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z\" clip-rule=\"evenodd\"></path></svg>",
            
            // 默认代码图标
            _ => GetLargeDefaultIcon()
        };
    }

    /// <summary>
    /// 获取默认文件图标 (小尺寸)
    /// </summary>
    private static string GetDefaultIcon()
    {
        return "<svg class=\"w-3.5 h-3.5 sm:w-4 sm:h-4 text-gray-400 group-hover:text-gray-600 flex-shrink-0 transition-colors\" fill=\"none\" stroke=\"currentColor\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z\"></path></svg>";
    }

    /// <summary>
    /// 获取默认文件图标 (大尺寸)
    /// </summary>
    private static string GetLargeDefaultIcon()
    {
        return "<svg class=\"w-6 h-6 text-gray-700 flex-shrink-0\" fill=\"none\" stroke=\"currentColor\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M10 20l4-16m4 4l4 4-4 4M6 16l-4-4 4-4\"></path></svg>";
    }
}
