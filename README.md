# 📱 HAHATalk (WPF MVVM Study & Extension Project)

**“5년 차 WinForms 개발자의 WPF MVVM 전환 및 실무 최적화 기록”**

본 프로젝트는 **까불이 코더**님의 WPF 카카오톡 클론 강의를 학습 기반으로 시작하여,  
실무 관점에서 **아키텍처 개선 및 기능 확장**을 진행하고 있는 프로젝트입니다.  
단순 UI 모방을 넘어 **MSSQL 연동, 안정적인 예외 처리, 비동기 구조 개선** 등에 중점을 두고 있습니다.

---

## 🛠 주요 기술 스택

| 영역       | 기술 / 설명 |
|-----------|-------------|
| Framework | .NET 8 / WPF |
| Pattern   | MVVM (CommunityToolkit.Mvvm) / Repository Pattern |
| Database  | MSSQL / Microsoft.Data.SqlClient |
| UI/UX     | Custom Control Templates, DataTriggers, Microsoft.Xaml.Behaviors |

---

## 📅 작업 기록 (Changelog 요약)

- **2026-03-20 | UI 최적화**
  - AllowsTransparency 사용 시 발생한 윈도우 레이아웃 문제 해결  
  - Window 속성 `SizeToContent="WidthAndHeight"` 적용으로 콘텐츠 동적 피팅

- **2026-03-19 | Repository 전환 및 타입 최적화**
  - 기존 MySQL 구조를 MSSQL 기반 Repository로 재설계  
  - 비동기 데이터 타입 불일치 문제 해결, 안정적 DB 호출 구현

- **2026-03-18 | 핵심 기능 구현**
  - Password Change: 현재 비밀번호 검증 및 신규 비밀번호 확인 로직  
  - Find Account: 이메일/사용자 정보 조회 및 ViewModel 설계  
  - Dictionary 기반 입력 필드 유효성 실시간 관리

---

## 🚀 향후 개발 계획 (Roadmap)

- **In Progress**: 1:1 채팅 시스템 (IChatRepository 기반 과거 대화 비동기 로드)  
- **Planned**: 이벤트 핸들링 고도화 (친구 목록 더블 클릭 → 채팅창 동적 생성)  
- **Planned**: 실시간 상태 동기화, ObservableCollection 최적화, 메신저 알림 기능

---

## 💡 개발자의 한마디

> “WinForms 경험을 바탕으로 WPF MVVM 전환을 진행하며,  
> 실무 문제 해결을 통해 관심사 분리(SoC)와 선언적 UI 설계의 실전 가치를 체득했습니다.  
> 단순히 코드를 따라 쓰는 것이 아니라, **‘왜 이 패턴이 필요한가?’**를 스스로 고민하며 구현했습니다.”

---

## 📌 주의 사항

- 초기 UI 및 구조는 까불이 코더님의 강의를 참고하였으며,  
- 이후 Repository, Store 기반 상태 관리, 비동기 처리 등 실무 최적화를 직접 적용하였습니다.  
- 모든 기능과 구조 개선은 본인이 구현한 내용임을 명확히 합니다.
