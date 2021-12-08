using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Urachacha
{
	public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

	    [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 50f, 0f);

	    [Tooltip("UI Text to display Player's Name")]
	    [SerializeField]
	    private Text playerNameText;

	    [Tooltip("UI Slider to display Player's Health")]
	    [SerializeField]
	    private Slider playerGuageSlider;

        PlayerManager target;

		float characterControllerHeight;

		Transform targetTransform;

		Renderer targetRenderer;

	    CanvasGroup _canvasGroup;
	    
		Vector3 targetPosition;

		#endregion

		#region MonoBehaviour Messages
		
		void Awake()
		{
			_canvasGroup = this.GetComponent<CanvasGroup>();
			
			this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
		}

		void Update()
		{
			if (target == null) {
				Destroy(this.gameObject);
				return;
			}

			if (playerGuageSlider != null) {
				playerGuageSlider.value = target.Gauge;
			}
		}

		void LateUpdate () 
		{
			if (targetRenderer!=null)
			{
				this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
			}
			
			if (targetTransform!=null)
			{
				targetPosition = targetTransform.position;
				targetPosition.y += characterControllerHeight;
				
				this.transform.position = Camera.main.WorldToScreenPoint (targetPosition) + screenOffset;
			}
		}

		#endregion

		#region Public Methods

		/// <param name="target">Target.</param>
		public void SetTarget(PlayerManager _target){

			if (_target == null) {
				Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
				return;
			}

			this.target = _target;
            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponentInChildren<Renderer>();


            CharacterController _characterController = this.target.GetComponent<CharacterController> ();

			if (_characterController != null){
				characterControllerHeight = _characterController.height;
			}

			if (playerNameText != null) {
                playerNameText.text = this.target.photonView.Owner.NickName;
			}
		}

		#endregion
	}
}