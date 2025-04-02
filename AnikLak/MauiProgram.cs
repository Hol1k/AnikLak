#if ANDROID
using Android.Widget;
#endif
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace AnikLak;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        EntryHandler.Mapper.AppendToMapping("CustomCursor", (handler, view) =>
        {
#if ANDROID
            if (handler.PlatformView is EditText nativeEditText)
            {
                nativeEditText.Background = null;

                nativeEditText.SetHighlightColor(Android.Graphics.Color.Gray);
            }
#endif
        });

		DatePickerHandler.Mapper.AppendToMapping("CustomCursor", (handler, view) =>
		{
#if ANDROID
            if (handler.PlatformView is EditText nativeEditText)
            {
                nativeEditText.Background = null;

                nativeEditText.SetHighlightColor(Android.Graphics.Color.Gray);
            }
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
