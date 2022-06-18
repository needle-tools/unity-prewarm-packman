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
		private static void Init()
		{
			if (PackManPreloaded) return;
			PackManPreloaded = true;
			if (Resources.FindObjectsOfTypeAll<PackageManagerWindow>().FirstOrDefault() != null) return;
			var window = EditorWindow.CreateWindow<PackageManagerWindow>();

			var frame = 0;
			void OnEditorUpdate()
			{
				if (frame++ < 30) return;
				if (window) window.Close();
				EditorApplication.update -= OnEditorUpdate;
			}

			EditorApplication.update += OnEditorUpdate;
		}
	}
}