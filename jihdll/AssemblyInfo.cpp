#include "pch.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;
using namespace System::Resources;

// アセンブリに関する一般情報は、次の一連の属性によって制御されます。
// アセンブリに関連付けられた情報を変更するには、これらの属性値を変更します。
[assembly:AssemblyTitleAttribute(L"jihdll")];
[assembly:AssemblyDescriptionAttribute(L"")] ;
[assembly:AssemblyConfigurationAttribute(L"")] ;
[assembly:AssemblyCompanyAttribute(L"")] ;
[assembly:AssemblyProductAttribute(L"DLL for JapaneseInputHelper")] ;
[assembly:AssemblyCopyrightAttribute(L"Copyright (C) 2024 Yukki")] ;
[assembly:AssemblyTrademarkAttribute(L"")] ;
[assembly:AssemblyCultureAttribute(L"")] ;
[assembly:NeutralResourcesLanguageAttribute(L"ja-JP")] ;

// ComVisibleをfalseに設定すると、このアセンブリ内の型がCOMコンポーネントに表示されなくなります。
// COMからこのアセンブリ内の型にアクセスする必要がある場合は、その型のComVisible属性をtrueに設定します。
[assembly:ComVisible(false)];

// 次の GUID は、このプロジェクトが COM に公開されている場合の typelib の ID です。
[assembly:Guid(L"206BCA23-64D3-4E61-B522-64EF4988BE63")] ;

// アセンブリのバージョン情報は、次の4つの値で構成されます。
//
//      メジャーバージョン
//      マイナーバージョン
//      ビルド番号
//      リビジョン
//
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// 注: バージョンを変更するときは、必ずセットアップ プロジェクトのバージョンも
// 更新してください。
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
[assembly:AssemblyVersionAttribute(L"1.0.0.0")];
[assembly:AssemblyFileVersionAttribute(L"1.0.0.0")] ;
