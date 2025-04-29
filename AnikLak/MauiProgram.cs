#if ANDROID
using Android.Widget;
using Microsoft.Maui.Handlers;
#endif
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AnikLak;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if ANDROID
        EntryHandler.Mapper.AppendToMapping("CustomCursor", (handler, view) =>
        {
            if (handler.PlatformView is EditText nativeEditText)
            {
                nativeEditText.Background = null;

                nativeEditText.SetHighlightColor(Android.Graphics.Color.Gray);
            }
        });

		DatePickerHandler.Mapper.AppendToMapping("CustomCursor", (handler, view) =>
		{
            if (handler.PlatformView is EditText nativeEditText)
            {
                nativeEditText.Background = null;

                nativeEditText.SetHighlightColor(Android.Graphics.Color.Gray);
            }
        });

        EditorHandler.Mapper.AppendToMapping("CustomCursor", (handler, view) =>
        {
            if (handler.PlatformView is EditText nativeEditText)
            {
                nativeEditText.Background = null;

                nativeEditText.SetHighlightColor(Android.Graphics.Color.Gray);
            }
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
