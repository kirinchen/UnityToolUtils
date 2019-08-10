using System;
using System.Collections;
using System.Threading;
using UniRx;

namespace UniRxExtensions {
    public static partial class ObservableEx {
        public static IObservable<Unit> ToObservableSafe(this IEnumerator coroutine) {
            return Observable.FromCoroutine<Unit>((observer, cancellationToken) => HandleExceptions(coroutine, observer, cancellationToken));
        }

        public static IObservable<Unit> FromCoroutineSafe(IEnumerator coroutine) {
            return Observable.FromCoroutine<Unit>((observer, cancellationToken) => HandleExceptions(coroutine, observer, cancellationToken));
        }

        public static IObservable<Unit> FromCoroutineSafe(Func<IEnumerator> coroutine) {
            return Observable.FromCoroutine<Unit>((observer, cancellationToken) => HandleExceptions(coroutine(), observer, cancellationToken));
        }

        public static IObservable<T> FromCoroutineSafe<T>(Func<IObserver<T>, IEnumerator> coroutine) {
            return Observable.FromCoroutine<T>((observer, cancellationToken) => HandleExceptions<T>(coroutine(observer), observer, cancellationToken));
        }

        public static IObservable<T> FromCoroutineSafe<T>(Func<IObserver<T>, CancellationToken, IEnumerator> coroutine) {
            return Observable.FromCoroutine<T>((observer, cancellationToken) => HandleExceptions<T>(coroutine(observer, cancellationToken), observer, cancellationToken));
        }

        public static IObservable<Unit> FromMicroCoroutineSafe(IEnumerator coroutine) {
            return Observable.FromMicroCoroutine<Unit>((observer, cancellationToken) => HandleExceptionsMicro(coroutine, observer, cancellationToken));
        }

        public static IObservable<Unit> FromMicroCoroutineSafe(Func<IEnumerator> coroutine) {
            return Observable.FromMicroCoroutine<Unit>((observer, cancellationToken) => HandleExceptionsMicro(coroutine(), observer, cancellationToken));
        }

        public static IObservable<T> FromMicroCoroutineSafe<T>(Func<IObserver<T>, IEnumerator> coroutine) {
            return Observable.FromMicroCoroutine<T>((observer, cancellationToken) => HandleExceptionsMicro<T>(coroutine(observer), observer, cancellationToken));
        }

        public static IObservable<T> FromMicroCoroutineSafe<T>(Func<IObserver<T>, CancellationToken, IEnumerator> coroutine) {
            return Observable.FromMicroCoroutine<T>((observer, cancellationToken) => HandleExceptionsMicro<T>(coroutine(observer, cancellationToken), observer, cancellationToken));
        }

        public static IObservable<string> ToObservableSafe() {
            return null;
        }

        static IEnumerator HandleExceptions(IEnumerator enumerator, IObserver<Unit> observer, CancellationToken cancellationToken) {
            using (enumerator as IDisposable) {
                while (true) {
                    bool hasNext = false;

                    if (cancellationToken.IsCancellationRequested) {
                        yield break;
                    }

                    try {
                        hasNext = enumerator.MoveNext();
                    } catch (Exception exception) {
                        observer.OnError(exception);
                        yield break;
                    }

                    if (hasNext) {
                        object obj = enumerator.Current;
                        var coroutine = obj as IEnumerator;
                        if (coroutine != null) {
                            yield return HandleExceptions<Unit>(coroutine, observer, cancellationToken);
                        } else {
                            yield return obj;
                        }
                    } else {
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                        yield break;
                    }
                }
            }
        }

        static IEnumerator HandleExceptions<T>(IEnumerator enumerator, IObserver<T> observer, CancellationToken cancellationToken) {
            using (enumerator as IDisposable) {
                while (true) {
                    bool hasNext = false;

                    if (cancellationToken.IsCancellationRequested) {
                        yield break;
                    }

                    try {
                        hasNext = enumerator.MoveNext();
                    } catch (Exception exception) {
                        observer.OnError(exception);
                        yield break;
                    }

                    if (hasNext) {
                        object obj = enumerator.Current;
                        var coroutine = obj as IEnumerator;
                        if (coroutine != null) {
                            yield return HandleExceptions<T>(coroutine, observer, cancellationToken);
                        } else {
                            yield return obj;
                        }
                    } else {
                        yield break;
                    }
                }
            }
        }

        static IEnumerator HandleExceptionsMicro(IEnumerator enumerator, IObserver<Unit> observer, CancellationToken cancellationToken) {
            using (enumerator as IDisposable) {
                while (true) {
                    bool hasNext = false;

                    if (cancellationToken.IsCancellationRequested) {
                        yield break;
                    }

                    try {
                        hasNext = enumerator.MoveNext();
                    } catch (Exception exception) {
                        observer.OnError(exception);
                        yield break;
                    }

                    if (hasNext) {
                        yield return enumerator.Current;
                    } else {
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                        yield break;
                    }
                }
            }
        }

        static IEnumerator HandleExceptionsMicro<T>(IEnumerator enumerator, IObserver<T> observer, CancellationToken cancellationToken) {
            using (enumerator as IDisposable) {
                while (true) {
                    bool hasNext = false;

                    if (cancellationToken.IsCancellationRequested) {
                        yield break;
                    }

                    try {
                        hasNext = enumerator.MoveNext();
                    } catch (Exception exception) {
                        observer.OnError(exception);
                        yield break;
                    }

                    if (hasNext) {
                        yield return enumerator.Current;
                    } else {
                        yield break;
                    }
                }
            }
        }
    }
}
