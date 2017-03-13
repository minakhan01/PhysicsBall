using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// GestureManager contains event handlers for subscribed gestures.
    /// </summary>
    public class GestureManager : MonoBehaviour
    {
        private GestureRecognizer gestureRecognizer;

        private GestureRecognizer BallForceRecognizer;

        private bool isApplyingForce;

        public GameObject forceBall;


        void Start()
        {
            Debug.Log("Start");
            gestureRecognizer = new GestureRecognizer();
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

            gestureRecognizer.TappedEvent += (source, tapCount, ray) =>
            {
                Debug.Log("gestureRecognizer");
            };

            gestureRecognizer.StartCapturingGestures();

            BallForceRecognizer = new GestureRecognizer();

            BallForceRecognizer.SetRecognizableGestures(GestureSettings.Hold
                | GestureSettings.ManipulationTranslate);

            BallForceRecognizer.ManipulationStartedEvent += BallForceRecognizer_ManipulationStartedEvent;
            BallForceRecognizer.ManipulationUpdatedEvent += BallForceRecognizer_ManipulationUpdatedEvent;
            BallForceRecognizer.ManipulationCompletedEvent += BallForceRecognizer_ManipulationCompletedEvent;
            BallForceRecognizer.ManipulationCanceledEvent += BallForceRecognizer_ManipulationCanceledEvent;
            //BallForceRecognizer.HoldStartedEvent += BallForceRecognizer_HoldStartedEvent;
            //BallForceRecognizer.HoldCompletedEvent += BallForceRecognizer_HoldCompletedEvent;
            //BallForceRecognizer.HoldCanceledEvent += BallForceRecognizer_HoldCancelledEvent;

            BallForceRecognizer.StartCapturingGestures();
        }

        void OnDestroy()
        {
            gestureRecognizer.StopCapturingGestures();
            BallForceRecognizer.StopCapturingGestures();
        }

        private void BallForceRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            Debug.Log("BallForceRecognizer_ManipulationStartedEvent");
            forceBall.GetComponent<ForceBallMover>().moveForceBall(position);
        }

        private void BallForceRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            Debug.Log("BallForceRecognizer_ManipulationUpdatedEvent");
            forceBall.GetComponent<ForceBallMover>().moveForceBall(position);

        }

        private void BallForceRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            Debug.Log("BallForceRecognizer_ManipulationCompletedEvent");
            isApplyingForce = false;
            forceBall.GetComponent<ForceBallMover>().applyForce();
        }

        private void BallForceRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            Debug.Log("BallForceRecognizer_ManipulationCanceledEvent");
            isApplyingForce = false;
        }

        //private void BallForceRecognizer_HoldStartedEvent(InteractionSourceKind source, Ray ray)
        //{
        //    Debug.Log("BallForceRecognizer_HoldStartedEvent");
        //}

        //private void BallForceRecognizer_HoldCompletedEvent(InteractionSourceKind source, Ray ray)
        //{
        //    Debug.Log("BallForceRecognizer_HoldCompletedEvent");
        //}

        //private void BallForceRecognizer_HoldCancelledEvent(InteractionSourceKind source, Ray ray)
        //{
        //    Debug.Log("BallForceRecognizer_HoldCancelledEvent");
        //}
    }
}

//using UnityEngine;
//using UnityEngine.VR.WSA.Input;

//namespace Academy.HoloToolkit.Unity
//{
//    /// <summary>
//    /// GestureManager contains event handlers for subscribed gestures.
//    /// </summary>
//    public class GestureManager : Singleton<GestureManager>
//    {
//        private GestureRecognizer gestureRecognizer;

//        public GestureRecognizer BallForceRecognizer { get; private set; }

//        public Vector3 BallForcePosition { get; private set; }

//        public bool IsApplyingForce { get; private set; }

//        // Tap and Navigation gesture recognizer.
//        public GestureRecognizer NavigationRecognizer { get; private set; }

//        // Manipulation gesture recognizer.
//        public GestureRecognizer ManipulationRecognizer { get; private set; }

//        // Currently active gesture recognizer.
//        public GestureRecognizer ActiveRecognizer { get; private set; }

//        public bool IsNavigating { get; private set; }

//        public Vector3 NavigationPosition { get; private set; }

//        public bool IsManipulating { get; private set; }

//        public Vector3 ManipulationPosition { get; private set; }

//        //void Awake()
//        //{
//        //    /* TODO: DEVELOPER CODING EXERCISE 2.b */

//        //    // 2.b: Instantiate the NavigationRecognizer.
//        //    NavigationRecognizer = new GestureRecognizer();

//        //    // 2.b: Add Tap and NavigationX GestureSettings to the NavigationRecognizer's RecognizableGestures.
//        //    NavigationRecognizer.SetRecognizableGestures(
//        //        GestureSettings.Tap |
//        //        GestureSettings.NavigationX);

//        //    // 2.b: Register for the TappedEvent with the NavigationRecognizer_TappedEvent function.
//        //    NavigationRecognizer.TappedEvent += NavigationRecognizer_TappedEvent;
//        //    // 2.b: Register for the NavigationStartedEvent with the NavigationRecognizer_NavigationStartedEvent function.
//        //    NavigationRecognizer.NavigationStartedEvent += NavigationRecognizer_NavigationStartedEvent;
//        //    // 2.b: Register for the NavigationUpdatedEvent with the NavigationRecognizer_NavigationUpdatedEvent function.
//        //    NavigationRecognizer.NavigationUpdatedEvent += NavigationRecognizer_NavigationUpdatedEvent;
//        //    // 2.b: Register for the NavigationCompletedEvent with the NavigationRecognizer_NavigationCompletedEvent function. 
//        //    NavigationRecognizer.NavigationCompletedEvent += NavigationRecognizer_NavigationCompletedEvent;
//        //    // 2.b: Register for the NavigationCanceledEvent with the NavigationRecognizer_NavigationCanceledEvent function. 
//        //    NavigationRecognizer.NavigationCanceledEvent += NavigationRecognizer_NavigationCanceledEvent;

//        //    // Instantiate the ManipulationRecognizer.
//        //    ManipulationRecognizer = new GestureRecognizer();

//        //    // Add the ManipulationTranslate GestureSetting to the ManipulationRecognizer's RecognizableGestures.
//        //    ManipulationRecognizer.SetRecognizableGestures(
//        //        GestureSettings.ManipulationTranslate);

//        //    // Register for the Manipulation events on the ManipulationRecognizer.
//        //    ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
//        //    ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
//        //    ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
//        //    ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;

//        //    ResetGestureRecognizers();
//        //}

//        void Start()
//        {
//            Debug.Log("Start");
//            gestureRecognizer = new GestureRecognizer();
//            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

//            gestureRecognizer.TappedEvent += (source, tapCount, ray) =>
//            {
//                Debug.Log("tapped event");
//                // GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

//                Debug.Log("tapped event");
//            };

//            gestureRecognizer.StartCapturingGestures();

//            BallForceRecognizer = new GestureRecognizer();

//            BallForceRecognizer.SetRecognizableGestures(GestureSettings.Hold
//                | GestureSettings.ManipulationTranslate);

//            BallForceRecognizer.ManipulationStartedEvent += BallForceRecognizer_ManipulationStartedEvent;
//            BallForceRecognizer.ManipulationUpdatedEvent += BallForceRecognizer_ManipulationUpdatedEvent;
//            BallForceRecognizer.ManipulationCompletedEvent += BallForceRecognizer_ManipulationCompletedEvent;
//            BallForceRecognizer.ManipulationCanceledEvent += BallForceRecognizer_ManipulationCanceledEvent;
//            BallForceRecognizer.HoldStartedEvent += BallForceRecognizer_HoldStartedEvent;
//            BallForceRecognizer.HoldCompletedEvent += BallForceRecognizer_HoldCompletedEvent;
//            BallForceRecognizer.HoldCanceledEvent += BallForceRecognizer_HoldCancelledEvent;

//            BallForceRecognizer.StartCapturingGestures();
//        }

//        void OnDestroy()
//        {
//            gestureRecognizer.StopCapturingGestures();

//            // 2.b: Unregister the Tapped and Navigation events on the NavigationRecognizer.
//            NavigationRecognizer.TappedEvent -= NavigationRecognizer_TappedEvent;

//            NavigationRecognizer.NavigationStartedEvent -= NavigationRecognizer_NavigationStartedEvent;
//            NavigationRecognizer.NavigationUpdatedEvent -= NavigationRecognizer_NavigationUpdatedEvent;
//            NavigationRecognizer.NavigationCompletedEvent -= NavigationRecognizer_NavigationCompletedEvent;
//            NavigationRecognizer.NavigationCanceledEvent -= NavigationRecognizer_NavigationCanceledEvent;

//            // Unregister the Manipulation events on the ManipulationRecognizer.
//            ManipulationRecognizer.ManipulationStartedEvent -= ManipulationRecognizer_ManipulationStartedEvent;
//            ManipulationRecognizer.ManipulationUpdatedEvent -= ManipulationRecognizer_ManipulationUpdatedEvent;
//            ManipulationRecognizer.ManipulationCompletedEvent -= ManipulationRecognizer_ManipulationCompletedEvent;
//            ManipulationRecognizer.ManipulationCanceledEvent -= ManipulationRecognizer_ManipulationCanceledEvent;

//            // Unregister the Manipulation events on the ManipulationRecognizer.
//            BallForceRecognizer.ManipulationStartedEvent -= BallForceRecognizer_ManipulationStartedEvent;
//            BallForceRecognizer.ManipulationUpdatedEvent -= BallForceRecognizer_ManipulationUpdatedEvent;
//            BallForceRecognizer.ManipulationCompletedEvent -= BallForceRecognizer_ManipulationCompletedEvent;
//            BallForceRecognizer.ManipulationCanceledEvent -= BallForceRecognizer_ManipulationCanceledEvent;
//            BallForceRecognizer.HoldStartedEvent -= BallForceRecognizer_HoldStartedEvent;
//            BallForceRecognizer.HoldCompletedEvent -= BallForceRecognizer_HoldCompletedEvent;
//            BallForceRecognizer.HoldCanceledEvent -= BallForceRecognizer_HoldCancelledEvent;
//        }

//        private void BallForceRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_ManipulationStartedEvent");

//        }

//        private void BallForceRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_ManipulationUpdatedEvent");

//        }

//        private void BallForceRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_ManipulationCompletedEvent");
//            IsApplyingForce = false;
//        }

//        private void BallForceRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_ManipulationCanceledEvent");
//            IsApplyingForce = false;
//        }

//        private void BallForceRecognizer_HoldStartedEvent(InteractionSourceKind source, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_HoldStartedEvent");
//        }

//        private void BallForceRecognizer_HoldCompletedEvent(InteractionSourceKind source, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_HoldCompletedEvent");
//        }

//        private void BallForceRecognizer_HoldCancelledEvent(InteractionSourceKind source, Ray ray)
//        {
//            Debug.Log("BallForceRecognizer_HoldCancelledEvent");
//        }

//        /// <summary>
//        /// Revert back to the default GestureRecognizer.
//        /// </summary>
//        public void ResetGestureRecognizers()
//        {
//            // Default to the navigation gestures.
//            Transition(NavigationRecognizer);
//        }

//        /// <summary>
//        /// Transition to a new GestureRecognizer.
//        /// </summary>
//        /// <param name="newRecognizer">The GestureRecognizer to transition to.</param>
//        public void Transition(GestureRecognizer newRecognizer)
//        {
//            Debug.Log("Transition");
//            if (newRecognizer == null)
//            {
//                return;
//            }

//            if (ActiveRecognizer != null)
//            {
//                if (ActiveRecognizer == newRecognizer)
//                {
//                    return;
//                }

//                ActiveRecognizer.CancelGestures();
//                ActiveRecognizer.StopCapturingGestures();
//            }

//            newRecognizer.StartCapturingGestures();
//            ActiveRecognizer = newRecognizer;
//        }

//        private void NavigationRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
//        {
//            // 2.b: Set IsNavigating to be true.
//            IsNavigating = true;

//            // 2.b: Set NavigationPosition to be relativePosition.
//            NavigationPosition = relativePosition;
//        }

//        private void NavigationRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
//        {
//            // 2.b: Set IsNavigating to be true.
//            IsNavigating = true;

//            // 2.b: Set NavigationPosition to be relativePosition.
//            NavigationPosition = relativePosition;
//        }

//        private void NavigationRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
//        {
//            // 2.b: Set IsNavigating to be false.
//            IsNavigating = false;
//        }

//        private void NavigationRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
//        {
//            // 2.b: Set IsNavigating to be false.
//            IsNavigating = false;
//        }

//        private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            if (HandsManager.Instance.FocusedGameObject != null)
//            {
//                IsManipulating = true;

//                ManipulationPosition = position;

//                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformManipulationStart", position);
//            }
//        }

//        private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            if (HandsManager.Instance.FocusedGameObject != null)
//            {
//                IsManipulating = true;

//                ManipulationPosition = position;

//                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformManipulationUpdate", position);
//            }
//        }

//        private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            IsManipulating = false;
//        }

//        private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
//        {
//            IsManipulating = false;
//        }

//        private void NavigationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
//        {
//            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;

//            if (focusedObject != null)
//            {
//                focusedObject.SendMessageUpwards("OnSelect");
//            }
//        }
//    }
//}