using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Needle
{
	internal static class FastifyPackMan
	{
		private static bool PackManPreloaded
		{
			get => SessionState.GetBool("Needle_PackMan_PreWarmed", false);
			set => SessionState.SetBool("Needle_PackMan_PreWarmed", value);
		}

		[InitializeOnLoadMethod]
		private static async void Init()
		{
			if (PackManPreloaded) return;
			PackManPreloaded = true;
			await Task.Delay(5);
			if (Resources.FindObjectsOfTypeAll<PackageManagerWindow>().FirstOrDefault() != null) return;
			var window = EditorWindow.CreateWindow<PackageManagerWindow>();
			await Task.Delay(5);
			if (window) window.Close();
		}
	}
}