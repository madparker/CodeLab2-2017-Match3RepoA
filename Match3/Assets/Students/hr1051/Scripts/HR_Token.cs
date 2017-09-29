using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {
	public class HR_Token : MonoBehaviour {
		[SerializeField] SpriteRenderer myColorRenderer;
		[SerializeField] SpriteRenderer myTextureRenderer;

		public void SetToken (int g_num) {
			myColorRenderer.color = HR_GameManagerScript.Instance.myTokenColors [g_num];
			myTextureRenderer.sprite = HR_GameManagerScript.Instance.myTokenTexture [g_num];
		}

	}
}
