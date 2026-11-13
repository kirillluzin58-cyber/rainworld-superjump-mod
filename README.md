# Super Jump — мод для Rain World

Простой кодовый мод для Rain World (нужен DLC **Downpour**): слизняк прыгает выше и бегает быстрее. Обе фичи настраиваются в игровом меню **Remix**.

## Что делает

| Настройка | По умолчанию | Эффект |
|---|---|---|
| Сила прыжка | 3 (0–10) | добавляет вертикальный импульс при прыжке с земли |
| Ускорение | вкл | +40% скорость бега, +30% скорость по шестам и в коридорах |

## Структура

```
assets/modinfo.json      — манифест мода (id, версия, зависимости)
src/Plugin.cs            — BepInEx-плагин, хуки (MonoMod) на Player.Update и SlugcatStats.ctor
src/Options.cs           — настройки для меню Remix (OptionInterface)
src/SuperJump.csproj     — проект; при наличии пути к игре сам копирует сборку в папку модов
lib/                     — сюда кладутся DLL из игры (в git не попадают)
```

## Сборка

1. Установи [.NET SDK](https://dotnet.microsoft.com/download) (любая версия 6+).
2. Скопируй из папки игры `RainWorld/RainWorld_Data/Managed/` в `lib/` файлы:
   - `Assembly-CSharp.dll`
   - `UnityEngine.dll`, `UnityEngine.CoreModule.dll`
   - `BepInEx.dll` (лежит в `RainWorld/BepInEx/core/`)
   - `Mono.Cecil.dll`, `MonoMod.*.dll` (в `RainWorld/BepInEx/patchers/MonoMod/` или `Managed/`)
3. Собери:
   ```
   dotnet build src/SuperJump.csproj -c Release
   ```
4. Если задана переменная окружения `RWGAME` (путь к папке игры), сборка сама скопируется в `RainWorld_Data/StreamingAssets/mods/SuperJump/`. Иначе скопируй вручную:
   ```
   RainWorld_Data/StreamingAssets/mods/SuperJump/
   ├── modinfo.json
   └── plugins/SuperJump.dll
   ```
5. Запусти игру → меню **Remix** → включи **Super Jump** → перезапусти игру.

## Заметки

- `target_game_version` в modinfo.json — `v1.9`; если у тебя Watcher/новее, поменяй на свою версию.
- Для публикации на Steam Workshop нужен `workshopid.json` с ID воркшоп-айтема.
- Хуки написаны через `On.*` (MonoMod RuntimeDetour) — стандартный для Rain World подход, совместимо с другими модами.
