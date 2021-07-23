using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XFABManager
{
    public class CustomAsyncOperation : CustomYieldInstruction
    {
        #region 字段 
        
        protected float _progress = 0;

        protected string _error = null;
        public event Action completed;

        private bool _isCompleted;

        #endregion

        #region 属性 

        public bool isDone { get { return isCompleted; } }


        protected bool isCompleted {
            get {
                return _isCompleted;
            }

            set {
                _isCompleted = value;
                if (_isCompleted) {
                    completed?.Invoke();
                    OnCompleted();
                }
            }
        }

        public float progress { get { return _progress; } }

        public string error { get { return _error; } }

        #endregion

        public override bool keepWaiting
        {
            get
            {
                return !isDone;
            }
        }

        protected virtual void OnCompleted() { 

        }

        protected void Completed(string error) {
            _error = error;
            isCompleted = true;
        }

        protected void Completed() {
            _progress = 1;

            if ( isCompleted ) return;
            isCompleted = true;
        }

    }

}
