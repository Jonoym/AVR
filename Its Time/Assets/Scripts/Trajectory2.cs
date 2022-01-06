// //======= Copyright (c) Valve Corporation, All rights reserved. ===============
// //
// // Purpose: Handles all the teleport logic
// //
// //=============================================================================

// using UnityEngine;
// using UnityEngine.Events;
// using System.Collections;

// namespace Valve.VR.InteractionSystem
// {
// 	//-------------------------------------------------------------------------
// 	public class Trajectory2 : MonoBehaviour
//     {
//         public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

//         public LayerMask traceLayerMask;
// 		public LayerMask floorFixupTraceLayerMask;
// 		public float floorFixupMaximumTraceDistance = 1.0f;
// 		public Material areaVisibleMaterial;
// 		public Material areaLockedMaterial;
// 		public Material areaHighlightedMaterial;
// 		public Material pointVisibleMaterial;
// 		public Material pointLockedMaterial;
// 		public Material pointHighlightedMaterial;
// 		public Transform destinationReticleTransform;
// 		public Transform invalidReticleTransform;
// 		public GameObject playAreaPreviewCorner;
// 		public GameObject playAreaPreviewSide;
// 		public Color pointerValidColor;
// 		public Color pointerInvalidColor;
// 		public Color pointerLockedColor;
// 		public bool showPlayAreaMarker = true;

// 		public float teleportFadeTime = 0.1f;
// 		public float meshFadeTime = 0.2f;

// 		public float arcDistance = 10.0f;

// 		[Header( "Effects" )]
// 		public Transform onActivateObjectTransform;
// 		public Transform onDeactivateObjectTransform;
// 		public float activateObjectTime = 1.0f;
// 		public float deactivateObjectTime = 1.0f;

// 		[Header( "Audio Sources" )]
// 		public AudioSource pointerAudioSource;
// 		public AudioSource loopingAudioSource;
// 		public AudioSource headAudioSource;
// 		public AudioSource reticleAudioSource;

// 		[Header( "Sounds" )]
// 		public AudioClip teleportSound;
// 		public AudioClip pointerStartSound;
// 		public AudioClip pointerLoopSound;
// 		public AudioClip pointerStopSound;
// 		public AudioClip goodHighlightSound;
// 		public AudioClip badHighlightSound;

// 		[Header( "Debug" )]
// 		public bool debugFloor = false;
// 		public bool showOffsetReticle = false;
// 		public Transform offsetReticleTransform;
// 		public MeshRenderer floorDebugSphere;
// 		public LineRenderer floorDebugLine;

// 		private LineRenderer pointerLineRenderer;
// 		private GameObject teleportPointerObject;
// 		private Transform pointerStartTransform;
// 		private Hand pointerHand = null;
// 		private Player player = null;
// 		private TrajectoryArc trajectoryArc = null;

// 		private bool visible = false;

// 		private TeleportMarkerBase[] teleportMarkers;
// 		private TeleportMarkerBase pointedAtTeleportMarker;
// 		private TeleportMarkerBase teleportingToMarker;
// 		private Vector3 pointedAtPosition;
// 		private Vector3 prevPointedAtPosition;
// 		private bool teleporting = false;
// 		private float currentFadeTime = 0.0f;

// 		private float meshAlphaPercent = 1.0f;
// 		private float pointerShowStartTime = 0.0f;
// 		private float pointerHideStartTime = 0.0f;
// 		private bool meshFading = false;
// 		private float fullTintAlpha;

// 		private float invalidReticleMinScale = 0.2f;
// 		private float invalidReticleMaxScale = 1.0f;
// 		private float invalidReticleMinScaleDistance = 0.4f;
// 		private float invalidReticleMaxScaleDistance = 2.0f;
// 		private Vector3 invalidReticleScale = Vector3.one;
// 		private Quaternion invalidReticleTargetRotation = Quaternion.identity;

// 		private Transform playAreaPreviewTransform;
// 		private Transform[] playAreaPreviewCorners;
// 		private Transform[] playAreaPreviewSides;

// 		private float loopingAudioMaxVolume = 0.0f;

// 		private Coroutine hintCoroutine = null;

// 		private bool originalHoverLockState = false;
// 		private Interactable originalHoveringInteractable = null;
// 		private AllowTeleportWhileAttachedToHand allowTeleportWhileAttached = null;

// 		private Vector3 startingFeetOffset = Vector3.zero;
// 		private bool movedFeetFarEnough = false;

// 		// Events

// 		public static SteamVR_Events.Event< float > ChangeScene = new SteamVR_Events.Event< float >();
// 		public static SteamVR_Events.Action< float > ChangeSceneAction( UnityAction< float > action ) { return new SteamVR_Events.Action< float >( ChangeScene, action ); }

// 		public static SteamVR_Events.Event< TeleportMarkerBase > Player = new SteamVR_Events.Event< TeleportMarkerBase >();
// 		public static SteamVR_Events.Action< TeleportMarkerBase > PlayerAction( UnityAction< TeleportMarkerBase > action ) { return new SteamVR_Events.Action< TeleportMarkerBase >( Player, action ); }

// 		public static SteamVR_Events.Event< TeleportMarkerBase > PlayerPre = new SteamVR_Events.Event< TeleportMarkerBase >();
// 		public static SteamVR_Events.Action< TeleportMarkerBase > PlayerPreAction( UnityAction< TeleportMarkerBase > action ) { return new SteamVR_Events.Action< TeleportMarkerBase >( PlayerPre, action ); }

// 		//-------------------------------------------------
// 		private static Trajectory2 _instance;
// 		public static Trajectory2 instance
// 		{
// 			get
// 			{
// 				if ( _instance == null )
// 				{
// 					_instance = GameObject.FindObjectOfType<Trajectory2>();
// 				}

// 				return _instance;
// 			}
// 		}


// 		//-------------------------------------------------
// 		void Awake()
//         {
//             _instance = this;


// 			pointerLineRenderer = GetComponentInChildren<LineRenderer>();
// 			teleportPointerObject = pointerLineRenderer.gameObject;

// #if UNITY_URP
// 			fullTintAlpha = 0.5f;
// #else
// 			int tintColorID = Shader.PropertyToID("_TintColor");
// 			fullTintAlpha = pointVisibleMaterial.GetColor(tintColorID).a;
// #endif

// 			trajectoryArc = GetComponent<TrajectoryArc>();
// 			trajectoryArc.traceLayerMask = traceLayerMask;

// 			loopingAudioMaxVolume = loopingAudioSource.volume;

// 			playAreaPreviewCorner.SetActive( false );
// 			playAreaPreviewSide.SetActive( false );

// 			float invalidReticleStartingScale = invalidReticleTransform.localScale.x;
// 			invalidReticleMinScale *= invalidReticleStartingScale;
// 			invalidReticleMaxScale *= invalidReticleStartingScale;
// 		}


// 		//-------------------------------------------------
// 		void Start()
//         {
//             teleportMarkers = GameObject.FindObjectsOfType<TeleportMarkerBase>();

// 			HidePointer();

// 			player = InteractionSystem.Player.instance;

// 			if ( player == null )
// 			{
// 				Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.", this);
// 				Destroy( this.gameObject );
// 				return;
// 			}
// 		}

// 		void OnDisable()
// 		{
// 			HidePointer();
// 		}


// 		//-------------------------------------------------
// 		private void CheckForSpawnPoint()
// 		{
// 			foreach ( TeleportMarkerBase teleportMarker in teleportMarkers )
// 			{
// 				TeleportPoint teleportPoint = teleportMarker as TeleportPoint;
// 				if ( teleportPoint && teleportPoint.playerSpawnPoint )
// 				{
// 					teleportingToMarker = teleportMarker;
// 					break;
// 				}
// 			}
// 		}


// 		//-------------------------------------------------
// 		public void HideTeleportPointer()
// 		{
// 			if ( pointerHand != null )
// 			{
// 				HidePointer();
// 			}
// 		}


// 		//-------------------------------------------------
// 		void Update()
// 		{
// 			Hand oldPointerHand = pointerHand;
// 			Hand newPointerHand = null;

// 			foreach ( Hand hand in player.hands )
// 			{
// 				if ( visible )
// 				{
// 					if ( WasFireButtonReleased( hand ) )
// 					{
// 						FindObjectOfType<FireBlock>().InitiateFire();
// 					}
// 				}
// 			}

// 			//If something is attached to the hand that is preventing teleport
// 			if ( allowTeleportWhileAttached && !allowTeleportWhileAttached.teleportAllowed )
// 			{
// 				HidePointer();
// 			}
// 			else
// 			{
// 				if ( !visible && newPointerHand != null )
// 				{
// 					//Begin showing the pointer
// 					ShowPointer( newPointerHand, oldPointerHand );
// 				}
// 				else if ( visible )
// 				{
// 					if ( newPointerHand == null && !IsFireButtonDown( pointerHand ) )
// 					{
// 						//Hide the pointer
// 						HidePointer();
// 					}
// 					else if ( newPointerHand != null )
// 					{
// 						//Move the pointer to a new hand
// 						ShowPointer( newPointerHand, oldPointerHand );
// 					}
// 				}
// 			}

// 			if ( visible )
// 			{
// 				UpdatePointer();

// 				if ( meshFading )
// 				{
// 					UpdateTeleportColors();
// 				}

// 				if ( onActivateObjectTransform.gameObject.activeSelf && Time.time - pointerShowStartTime > activateObjectTime )
// 				{
// 					onActivateObjectTransform.gameObject.SetActive( false );
// 				}
// 			}
// 			else
// 			{
// 				if ( onDeactivateObjectTransform.gameObject.activeSelf && Time.time - pointerHideStartTime > deactivateObjectTime )
// 				{
// 					onDeactivateObjectTransform.gameObject.SetActive( false );
// 				}
// 			}
// 		}


// 		//-------------------------------------------------
// 		private void UpdatePointer()
// 		{
// 			Vector3 pointerStart = pointerStartTransform.position;
// 			Vector3 pointerEnd;
// 			Vector3 pointerDir = pointerStartTransform.forward;
// 			bool hitSomething = false;
// 			Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;

// 			Vector3 arcVelocity = pointerDir * arcDistance;

// 			TeleportMarkerBase hitTeleportMarker = null;

// 			//Check pointer angle
// 			float dotUp = Vector3.Dot( pointerDir, Vector3.up );
// 			float dotForward = Vector3.Dot( pointerDir, player.hmdTransform.forward );
// 			bool pointerAtBadAngle = false;
// 			if ( ( dotForward > 0 && dotUp > 0.75f ) || ( dotForward < 0.0f && dotUp > 0.5f ) )
// 			{
// 				pointerAtBadAngle = true;
// 			}

// 			//Trace to see if the pointer hit anything
// 			RaycastHit hitInfo;
// 			trajectoryArc.SetArcData( pointerStart, arcVelocity, true, pointerAtBadAngle );
// 			if ( trajectoryArc.DrawArc( out hitInfo ) )
// 			{
// 				hitSomething = true;
// 				hitTeleportMarker = hitInfo.collider.GetComponentInParent<TeleportMarkerBase>();
// 			}

// 			if ( pointerAtBadAngle )
// 			{
// 				hitTeleportMarker = null;
// 			}

// 			if (true) //Hit neither
// 			{
// 				destinationReticleTransform.gameObject.SetActive( false );
// 				offsetReticleTransform.gameObject.SetActive( false );

// 				trajectoryArc.SetColor( pointerInvalidColor );
// #if (UNITY_5_4)
// 				pointerLineRenderer.SetColors( pointerInvalidColor, pointerInvalidColor );
// #else
// 				pointerLineRenderer.startColor = pointerInvalidColor;
// 				pointerLineRenderer.endColor = pointerInvalidColor;
// #endif
// 				//Orient the invalid reticle to the normal of the trace hit point
// 				Vector3 normalToUse = hitInfo.normal;
// 				float angle = Vector3.Angle( hitInfo.normal, Vector3.up );
// 				if ( angle < 15.0f )
// 				{
// 					normalToUse = Vector3.up;
// 				}
// 				invalidReticleTargetRotation = Quaternion.FromToRotation( Vector3.up, normalToUse );
// 				invalidReticleTransform.rotation = Quaternion.Slerp( invalidReticleTransform.rotation, invalidReticleTargetRotation, 0.1f );

// 				pointedAtTeleportMarker = null;

// 				if ( hitSomething )
// 				{
// 					pointerEnd = hitInfo.point;
// 				}
// 				else
// 				{
// 					pointerEnd = trajectoryArc.GetArcPositionAtTime( trajectoryArc.arcDuration );
// 				}

// 				//Debug floor
// 				if ( debugFloor )
// 				{
// 					floorDebugSphere.gameObject.SetActive( false );
// 					floorDebugLine.gameObject.SetActive( false );
// 				}
// 			}

// 			if ( !showOffsetReticle )
// 			{
// 				offsetReticleTransform.gameObject.SetActive( false );
// 			}

// 			destinationReticleTransform.position = pointedAtPosition;
// 			invalidReticleTransform.position = pointerEnd;
// 			onActivateObjectTransform.position = pointerEnd;
// 			onDeactivateObjectTransform.position = pointerEnd;
// 			offsetReticleTransform.position = pointerEnd - playerFeetOffset;

// 			reticleAudioSource.transform.position = pointedAtPosition;

// 			pointerLineRenderer.SetPosition( 0, pointerStart );
// 			pointerLineRenderer.SetPosition( 1, pointerEnd );
// 		}


// 		//-------------------------------------------------
// 		void FixedUpdate()
// 		{
// 			if ( !visible )
// 			{
// 				return;
// 			}

// 			if ( debugFloor )
// 			{
// 				//Debug floor
// 				TeleportArea teleportArea = pointedAtTeleportMarker as TeleportArea;
// 				if ( teleportArea != null )
// 				{
// 					if ( floorFixupMaximumTraceDistance > 0.0f )
// 					{
// 						floorDebugSphere.gameObject.SetActive( true );
// 						floorDebugLine.gameObject.SetActive( true );

// 						RaycastHit raycastHit;
// 						Vector3 traceDir = Vector3.down;
// 						traceDir.x = 0.01f;
// 						if ( Physics.Raycast( pointedAtPosition + 0.05f * traceDir, traceDir, out raycastHit, floorFixupMaximumTraceDistance, floorFixupTraceLayerMask ) )
// 						{
// 							floorDebugSphere.transform.position = raycastHit.point;
// 							floorDebugSphere.material.color = Color.green;
// #if (UNITY_5_4)
// 							floorDebugLine.SetColors( Color.green, Color.green );
// #else
// 							floorDebugLine.startColor = Color.green;
// 							floorDebugLine.endColor = Color.green;
// #endif
// 							floorDebugLine.SetPosition( 0, pointedAtPosition );
// 							floorDebugLine.SetPosition( 1, raycastHit.point );
// 						}
// 						else
// 						{
// 							Vector3 rayEnd = pointedAtPosition + ( traceDir * floorFixupMaximumTraceDistance );
// 							floorDebugSphere.transform.position = rayEnd;
// 							floorDebugSphere.material.color = Color.red;
// #if (UNITY_5_4)
// 							floorDebugLine.SetColors( Color.red, Color.red );
// #else
// 							floorDebugLine.startColor = Color.red;
// 							floorDebugLine.endColor = Color.red;
// #endif
// 							floorDebugLine.SetPosition( 0, pointedAtPosition );
// 							floorDebugLine.SetPosition( 1, rayEnd );
// 						}
// 					}
// 				}
// 			}
// 		}

// 		//-------------------------------------------------
// 		private void HidePointer()
// 		{
// 			if ( visible )
// 			{
// 				pointerHideStartTime = Time.time;
// 			}

// 			visible = false;
// 			if ( pointerHand )
// 			{
// 				if ( ShouldOverrideHoverLock() )
// 				{
// 					//Restore the original hovering interactable on the hand
// 					if ( originalHoverLockState == true )
// 					{
// 						pointerHand.HoverLock( originalHoveringInteractable );
// 					}
// 					else
// 					{
// 						pointerHand.HoverUnlock( null );
// 					}
// 				}

// 				//Stop looping sound
// 				loopingAudioSource.Stop();
// 			}
// 			teleportPointerObject.SetActive( false );

// 			trajectoryArc.Hide();

// 			foreach ( TeleportMarkerBase teleportMarker in teleportMarkers )
// 			{
// 				if ( teleportMarker != null && teleportMarker.markerActive && teleportMarker.gameObject != null )
// 				{
// 					teleportMarker.gameObject.SetActive( false );
// 				}
// 			}

// 			destinationReticleTransform.gameObject.SetActive( false );
// 			invalidReticleTransform.gameObject.SetActive( false );
// 			offsetReticleTransform.gameObject.SetActive( false );

// 			if ( playAreaPreviewTransform != null )
// 			{
// 				playAreaPreviewTransform.gameObject.SetActive( false );
// 			}

// 			if ( onActivateObjectTransform.gameObject.activeSelf )
// 			{
// 				onActivateObjectTransform.gameObject.SetActive( false );
// 			}
// 			onDeactivateObjectTransform.gameObject.SetActive( true );

// 			pointerHand = null;
// 		}


// 		//-------------------------------------------------
// 		private void ShowPointer( Hand newPointerHand, Hand oldPointerHand )
// 		{
// 			if ( !visible )
// 			{
// 				pointedAtTeleportMarker = null;
// 				pointerShowStartTime = Time.time;
// 				visible = true;
// 				meshFading = true;

// 				teleportPointerObject.SetActive( false );
// 				trajectoryArc.Show();

// 				foreach ( TeleportMarkerBase teleportMarker in teleportMarkers )
// 				{
// 					if ( teleportMarker.markerActive && teleportMarker.ShouldActivate( player.feetPositionGuess ) )
// 					{
// 						teleportMarker.gameObject.SetActive( true );
// 						teleportMarker.Highlight( false );
// 					}
// 				}

// 				startingFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
// 				movedFeetFarEnough = false;

// 				if ( onDeactivateObjectTransform.gameObject.activeSelf )
// 				{
// 					onDeactivateObjectTransform.gameObject.SetActive( false );
// 				}
// 				onActivateObjectTransform.gameObject.SetActive( true );

// 				loopingAudioSource.clip = pointerLoopSound;
// 				loopingAudioSource.loop = true;
// 				loopingAudioSource.Play();
// 				loopingAudioSource.volume = 0.0f;
// 			}


// 			if ( oldPointerHand )
// 			{
// 				if ( ShouldOverrideHoverLock() )
// 				{
// 					//Restore the original hovering interactable on the hand
// 					if ( originalHoverLockState == true )
// 					{
// 						oldPointerHand.HoverLock( originalHoveringInteractable );
// 					}
// 					else
// 					{
// 						oldPointerHand.HoverUnlock( null );
// 					}
// 				}
// 			}

// 			pointerHand = newPointerHand;

// 			if ( pointerHand )
// 			{
// 				pointerStartTransform = GetPointerStartTransform( pointerHand );

// 				if ( pointerHand.currentAttachedObject != null )
// 				{
// 					allowTeleportWhileAttached = pointerHand.currentAttachedObject.GetComponent<AllowTeleportWhileAttachedToHand>();
// 				}

// 				//Keep track of any existing hovering interactable on the hand
// 				originalHoverLockState = pointerHand.hoverLocked;
// 				originalHoveringInteractable = pointerHand.hoveringInteractable;

// 				if ( ShouldOverrideHoverLock() )
// 				{
// 					pointerHand.HoverLock( null );
// 				}

// 				pointerAudioSource.transform.SetParent( pointerStartTransform );
// 				pointerAudioSource.transform.localPosition = Vector3.zero;

// 				loopingAudioSource.transform.SetParent( pointerStartTransform );
// 				loopingAudioSource.transform.localPosition = Vector3.zero;
// 			}
// 		}


// 		//-------------------------------------------------
// 		private void UpdateTeleportColors()
// 		{
// 			float deltaTime = Time.time - pointerShowStartTime;
// 			if ( deltaTime > meshFadeTime )
// 			{
// 				meshAlphaPercent = 1.0f;
// 				meshFading = false;
// 			}
// 			else
// 			{
// 				meshAlphaPercent = Mathf.Lerp( 0.0f, 1.0f, deltaTime / meshFadeTime );
// 			}

// 			//Tint color for the teleport points
// 			foreach ( TeleportMarkerBase teleportMarker in teleportMarkers )
// 			{
// 				teleportMarker.SetAlpha( fullTintAlpha * meshAlphaPercent, meshAlphaPercent );
// 			}
// 		}

// 		//-------------------------------------------------
// 		public bool IsEligibleForFire( Hand hand )
// 		{
// 			if ( hand == null )
// 			{
// 				return false;
// 			}

// 			if ( !hand.gameObject.activeInHierarchy )
// 			{
// 				return false;
// 			}

// 			if ( hand.hoveringInteractable != null )
// 			{
// 				return false;
// 			}

// 			if ( hand.noSteamVRFallbackCamera == null )
// 			{
// 				if ( hand.isActive == false)
// 				{
// 					return false;
// 				}

// 				//Something is attached to the hand
// 				if ( hand.currentAttachedObject != null )
// 				{
// 					AllowTeleportWhileAttachedToHand allowTeleportWhileAttachedToHand = hand.currentAttachedObject.GetComponent<AllowTeleportWhileAttachedToHand>();

// 					if ( allowTeleportWhileAttachedToHand != null && allowTeleportWhileAttachedToHand.teleportAllowed == true )
// 					{
// 						return true;
// 					}
// 					else
// 					{
// 						return false;
// 					}
// 				}
// 			}

// 			return true;
// 		}


// 		//-------------------------------------------------
// 		private bool ShouldOverrideHoverLock()
// 		{
// 			if ( !allowTeleportWhileAttached || allowTeleportWhileAttached.overrideHoverLock )
// 			{
// 				return true;
// 			}

// 			return false;
// 		}


// 		//-------------------------------------------------
// 		private bool WasFireButtonReleased( Hand hand )
// 		{
// 			if ( IsEligibleForFire( hand ) )
// 			{
// 				if ( hand.noSteamVRFallbackCamera != null )
// 				{
// 					return Input.GetKeyUp( KeyCode.T );
// 				}
// 				else
//                 {
//                     return teleportAction.GetStateUp(hand.handType);

//                     //return hand.controller.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad );
//                 }
// 			}

// 			return false;
// 		}

// 		//-------------------------------------------------
// 		private bool IsFireButtonDown( Hand hand )
// 		{
// 			if ( IsEligibleForFire( hand ) )
// 			{
// 				if ( hand.noSteamVRFallbackCamera != null )
// 				{
// 					return Input.GetKey( KeyCode.T );
// 				}
// 				else
//                 {
//                     return teleportAction.GetState(hand.handType);
// 				}
// 			}

// 			return false;
// 		}


// 		//-------------------------------------------------
// 		private bool WasFireButtonPressed( Hand hand )
// 		{
// 			if ( IsEligibleForFire( hand ) )
// 			{
// 				if ( hand.noSteamVRFallbackCamera != null )
// 				{
// 					return Input.GetKeyDown( KeyCode.T );
// 				}
// 				else
//                 {
//                     return teleportAction.GetStateDown(hand.handType);

//                     //return hand.controller.GetPressDown( SteamVR_Controller.ButtonMask.Touchpad );
// 				}
// 			}

// 			return false;
// 		}


// 		//-------------------------------------------------
// 		private Transform GetPointerStartTransform( Hand hand )
// 		{
// 			if ( hand.noSteamVRFallbackCamera != null )
// 			{
// 				return hand.noSteamVRFallbackCamera.transform;
// 			}
// 			else
// 			{
// 				return hand.transform;
// 			}
// 		}
// 	}
// }
