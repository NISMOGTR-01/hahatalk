# 📱 HAHATalk (WPF MVVM Study & Extension Project)

> **"5년 차 WinForms 개발자의 WPF MVVM 패러다임 전환 및 실무 최적화 기록"**

본 프로젝트는 **'까불이 코더'**님의 WPF 카카오톡 클론 코딩 프로젝트를 마중물 삼아, 실무적인 관점에서 **아키텍처 개선 및 기능 확장**을 진행하고 있는 프로젝트입니다. 단순 UI 모방을 넘어, **현업 환경에 최적화된 MSSQL 연동과 안정적인 예외 처리 구조**를 구축하는 데 중점을 두었습니다.

---

## 🛠 주요 기술 스택
* **Framework**: .NET 8 / WPF (Windows Presentation Foundation)
* **Pattern**: MVVM (CommunityToolkit.Mvvm) / Repository Pattern
* **Database**: **MSSQL** (Remote Server) / Microsoft.Data.SqlClient
* **UI/UX**: Custom Control Templates, DataTriggers, Microsoft.Xaml.Behaviors

---

## 📅 상세 작업 및 트러블슈팅 히스토리 (Changelog)

### **2026-03-20: UI 정밀 최적화 및 DB 안정성 확보**
* **[Layout] 투명 윈도우 레이아웃 벌어짐 해결**
  - **이슈**: `AllowsTransparency="True"` 사용 시 윈도우 프레임과 내부 콘텐츠 사이의 유격 발생 및 마우스 클릭 간섭 현상.
  - **해결**: `Window` 속성에 `SizeToContent="WidthAndHeight"`를 적용하여 콘텐츠(`Border`) 크기에 윈도우가 동적으로 피팅되도록 개선.
* **[DB] 외래 키(FK) 제약 조건 및 데이터 무결성 강화**
  - **이슈**: 존재하지 않는 계정 추가 시 DB 제약 조건 위반으로 인한 런타임 에러 발생.
  - **해결**: 저장 전 `IsFriendAlreadyExistsAsync` 검증 로직을 ViewModel 단계에 추가하고, `SqlException` 예외 처리를 세분화하여 시스템 안정성 확보.


### **2026-03-19: 데이터 접근 계층(Repository) 전환 및 타입 최적화**
* **[Arch] MSSQL 환경 전면 재설계**
  - 기존 MySQL 기반 구조를 실무 환경에 적합한 **MSSQL**로 전환하고, `IAccountRepository` 인터페이스를 통해 데이터 접근 계층을 추상화.
* **[Refactoring] 비동기 데이터 타입 일치화**
  - 커스텀 DB 라이브러리의 `Execute` 반환값(`long`)과 로직 내 타입 불일치 문제를 해결하여 비동기 처리의 신뢰도 향상.

### **2026-03-18: 핵심 비즈니스 로직 및 유효성 검사 구현**
* **[Feat] 비밀번호 변경 (Password Change) 기능**
  - 현재 비밀번호 검증 및 신규 비밀번호 일치 확인 로직 구현.
  - `Dictionary<string, bool>` 바인딩을 활용하여 각 입력 필드의 유효성 상태를 실시간으로 관리 및 UI 피드백 구현.
* **[Feat] 계정 찾기 (Find Account) 기능**
  - 이메일 및 사용자 정보를 기반으로 한 조회 쿼리 연동 및 전용 ViewModel 설계.

---

## 🚀 향후 개발 계획 (Roadmap)

* **[In Progress] 1:1 채팅 시스템**: `IChatRepository`를 통한 DB 기반 과거 대화 이력 비동기 로드.
* **[Planned] 이벤트 핸들링 고도화**: 친구 목록 **더블 클릭 시**, 고유 세션 ID를 기반으로 한 채팅창 동적 생성 및 데이터 바인딩.
* **[Planned] 실시간 상태 동기화**: `ObservableCollection` 최적화 및 메신저 알림 기능 구현.

---

## 💡 개발자의 한마디
> "WinForms의 직관적인 개발 경험을 토대로, WPF의 **관심사 분리(SoC)**와 **선언적 UI**의 강력함을 실전 이슈 해결을 통해 체득하고 있습니다. 단순히 코드를 따라 쓰는 것이 아니라, **'왜 이 패턴이 필요한가?'**에 대해 스스로 답을 찾아가는 과정을 기록하고 있습니다."
