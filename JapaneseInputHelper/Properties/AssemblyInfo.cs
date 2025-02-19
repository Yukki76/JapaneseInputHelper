using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// アセンブリに関する一般的な情報は、次の方法で制御されます
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更します。
[assembly: AssemblyTitle("JapaneseInputHelper")]
[assembly: AssemblyDescription("Ctrl+¥で日本語入力切替ツール")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("JapaneseInputHelper")]
[assembly: AssemblyCopyright("Copyright (C) 2024-2025 Yukki")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、このアセンブリ内の型は COM コンポーネントから
// 参照できなくなります。COM からこのアセンブリ内の型にアクセスする必要がある場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("b657bebb-5b8c-4edd-bf61-be66caac7ef7")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、次を使用してビルド番号とリビジョン番号を既定に設定できます
// 既定値にすることができます:
[assembly: AssemblyVersion("2.1.0.1")]
[assembly: AssemblyFileVersion("2.0.0.1")]
[assembly: NeutralResourcesLanguage("ja")]

namespace Propeerties {
    internal class AssemblyInfo {

        #region プロパティ(Private)

        private static Type GetAssemblyInfo<Type>() {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(Type), false);
            if (attributes.Length == 0)
                return default;
            return ((Type)attributes[0]);
        }

        /// <summary>
        /// プログラムの詳細な説明
        /// </summary>
        public static string AssemblyDescription => GetAssemblyInfo<AssemblyDescriptionAttribute>().Description;

        /// <summary>
        /// 著作権情報
        /// </summary>
        public static string AssemblyCopyright => GetAssemblyInfo<AssemblyCopyrightAttribute>().Copyright;

        /// <summary>
        /// 製品名情報
        /// </summary>
        public static string AssemblyProduct => GetAssemblyInfo<AssemblyProductAttribute>().Product;

        /// <summary>
        /// 製品のバージョン情報
        /// </summary>
        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #endregion

    }
}
