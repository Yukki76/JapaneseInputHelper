using System.Security.Principal;

namespace Utils {
    internal class Common {
        /// <summary>
        /// 管理者モードで実行しているか確認
        /// </summary>
        /// <returns>
        /// true:  管理者モード<br/>
        /// false: 管理者モードでない
        /// </returns>
        static public bool IsAdministrator() {
            return new WindowsPrincipal(
                WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
